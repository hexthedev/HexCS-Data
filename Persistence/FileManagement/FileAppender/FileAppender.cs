using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HexCS.Data.Persistence
{
    public class FileAppender<TObj> : IFileAppender<TObj>, IDisposable
    {
        private IStreamAppender<TObj> _appender;
        private ConcurrentQueue<TObj> _write_queue = new ConcurrentQueue<TObj>();

        //object _active_stream_lock = new object();
        private StreamWriter _active_stream;
        private readonly CancellationTokenSource _cancel_source = new CancellationTokenSource();
        private readonly AutoResetEvent _write_wait_event = new AutoResetEvent(false);

        public int active_stream = 0;
        public int writes = 0;

        public FileAppender(FileInfo file, Encoding encoding, IStreamAppender<TObj> appender)
        {
            file.ExistsOrCreate();

            _active_stream = new StreamWriter(file.OpenWrite(), encoding);
            _write_wait_event.Set();

            _appender = appender;

            Task.Factory.StartNew(AppendTask);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _cancel_source.Cancel();
            _write_wait_event.Set();
            _active_stream.Close();
        }

        /// <inheritdoc />
        public void EnqueueObject(TObj ob)
        {
            _write_queue.Enqueue(ob);
            _write_wait_event.Set();
        }

        private void AppendTask()
        {
            while (!_cancel_source.Token.IsCancellationRequested)
            {
                _write_wait_event.WaitOne(Timeout.Infinite);

                if (_active_stream != null)
                {

                    TObj next_write;

                    if (_write_queue.TryDequeue(out next_write))
                    {
                        _appender.AppendToStream(next_write, _active_stream);
                        writes++;
                    }
                    else
                    {
                        _write_wait_event.Reset();
                    }

                    active_stream++;
                }
            }
        }


    }
}