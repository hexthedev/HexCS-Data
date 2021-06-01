using HexCS.Core;

using System.IO;

namespace HexCS.Data.Persistence
{
    public static class UTPathString_ReadWrite
    {
        /// <summary>
        /// Reads the file at the path string and returns
        /// the contents. Returns null if nothing is read
        /// </summary>
        public static string ReadFile(this PathString path)
        {
            if (!path.TryAsFileStream(FileMode.Open, out FileStream stream)) return null;
            return UTFileStream.ReadText(stream);
        }
    }
}