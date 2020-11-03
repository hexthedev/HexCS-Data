using System.IO;

namespace HexCS.Data.Persistence
{
    /// <summary>
    /// Handles logic to Save, Load and Create Empty persisted files
    /// of an object
    /// </summary>
    /// <typeparam name="T">Object to Save, Load and Create Empty</typeparam>
    public interface IFileManager<T> : IFileLoader<T>, IFileSaver<T>
    {

        /// <summary>
        /// Creates and saves a file representing an empty object
        /// Used to create initial files in some situations. 
        /// </summary>
        /// <param name="file">Path to file</param>
        /// <returns>True is successfully created</returns>
        bool CreateEmpty(FileInfo file);

    }
}