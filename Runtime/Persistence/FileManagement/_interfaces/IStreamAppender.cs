using System.IO;

namespace HexCS.Data.Persistence
{

    /// <summary>
    /// Appends object to a stream
    /// </summary>
    /// <typeparam name="TObj">Object to append</typeparam>
    public interface IStreamAppender<TObj>
    {
        bool AppendToStream(TObj obj, StreamWriter stream);
    }
}