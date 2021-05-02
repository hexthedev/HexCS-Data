using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HexCS.Data.Persistence
{
    /// <summary>
    /// General utilities foe FIleInfo objects
    /// </summary>
    public static class UTFileInfo
    {
        /// <summary>
        /// Checks if a File exists, if it doesn't funciton creates it. Automatically creates 
        /// folder structure if folder dosen't exist
        /// </summary>
        /// <param name="file"></param>
        /// <returns>True if file previously existed</returns>
        public static void ExistsOrCreate(this FileInfo file)
        {
            file.Directory?.ExistsOrCreate();

            if (!file.Exists)
            {
                using (file.Create()) { }
            }
        }

        /// <summary>
        /// Checks if a File exists, if it doesn't funciton creates it.
        /// If it does, delete everything inside it
        /// </summary>
        /// <param name="file"></param>
        /// <returns>True if file previously existed</returns>
        public static bool ForceEmptyOrCreate(this FileInfo file)
        {
            if (!file.Exists)
            {
                using (file.Create()) { }
                return false;
            }
            else
            {
                file.Delete();
                using (file.Create()) { }
                return true;
            }
        }

        /// <summary>
        /// Opens a file and returns text as a string
        /// </summary>
        /// <param name="file">File to Read from</param>
        /// <returns>string read from file</returns>
        public static string ReadAllText(this FileInfo file)
        {
            using (StreamReader f = file.OpenText())
            {
                return f.ReadToEnd();
            }
        }

        /// <summary>
        /// <para>Reads a file info file line by line and returns a string[].</para>
        /// <para>The current implementation
        /// uses a list so creates some garbage which may not be ideal if this is called often. </para>
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string[] ReadAllLines(this FileInfo file)
        {
            List<string> lines = new List<string>();

            using (StreamReader f = file.OpenText())
            {
                string line = string.Empty;
                while ((line = f.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines.ToArray();
        }

        /// <summary>
        /// Write a string to a file
        /// </summary>
        /// <param name="text">string to write</param>
        /// <param name="file">file to write to</param>
        /// <param name="encoding">encoding of string</param>
        public static void WriteString(this FileInfo file, string text, Encoding encoding) => file.WriteBytes(encoding.GetBytes(text));

        /// <summary>
        /// Write a byte[] to a file
        /// </summary>
        /// <param name="data">string to write</param>
        /// <param name="file">file to write to</param>
        public static void WriteBytes(this FileInfo file, byte[] data)
        {
            using (FileStream f = file.OpenWrite())
            {
                f.Write(data, 0, data.Length);
            }
        }
    }
}