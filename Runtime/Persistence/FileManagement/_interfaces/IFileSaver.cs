using System.IO;

namespace HexCS.Data.Persistence
{
    public interface IFileSaver<T>
    {

        /// <summary>
        /// Save obj to a file
        /// </summary>
        /// <param name="obj">Object to save to file</param>
        /// <param name="file_name">Name of the file without extention</param>
        /// <param name="path">Path to directory in which to save file</param>
        /// <param name="force_extension">if not null, will replace default file extension</param>
        bool SaveFile(T obj, FileInfo file_info);

    }
}