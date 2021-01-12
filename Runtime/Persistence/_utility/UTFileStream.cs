using System.IO;
using System.Text;

namespace HexCS.Data.Persistence
{
    /// <summary>
    /// General utilities for file stream objects
    /// </summary>
    public static class UTFileStream
    {
        /// <summary>
        /// Write a string to a file using a pre opened stream. Does not dispose
        /// </summary>
        /// <param name="text">string to write</param>
        /// <param name="stream">stream to write to</param>
        /// <param name="encoding">endoing</param>
        public static void WriteString(this FileStream stream, string text, Encoding encoding)
        {
            stream.Write(encoding.GetBytes(text), 0, text.Length);
        }
    }
}
