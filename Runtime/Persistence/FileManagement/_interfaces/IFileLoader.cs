using System.IO;

namespace HexCS.Data.Persistence
{
    public interface IFileLoader<T>
    {

        /// <summary>
        /// Load file to runtime type T
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns>Loaded file</returns>
        T LoadFile(FileInfo path);

    }
}