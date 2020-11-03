namespace HexCS.Data.Persistence
{
    /// <summary>
    /// File appenders can open or create a file and continously append information to it. 
    /// </summary>
    /// <typeparam name="TObj"></typeparam>
    public interface IFileAppender<TObj>
    {

        /// <summary>
        /// Queue object for writting
        /// </summary>
        /// <param name="ob"></param>
        void EnqueueObject(TObj ob);

    }
}