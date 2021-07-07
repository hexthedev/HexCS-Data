using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HexCS.Data.Persistence
{
    /// <summary>
    /// Class containing general useful functions for paths
    /// </summary>
    public static class UTDirectoryInfo
    {
        #region API
        /// <summary>
        /// Checks if a Directory exists, if it doesn't create it
        /// </summary>
        /// <param name="directory">Path to check or create</param>
        /// <returns>true if directory exists</returns>
        public static void ExistsOrCreate(this DirectoryInfo directory)
        {
            if (!directory.Exists)
            {
                directory.Create();
            }
        }

        /// <summary>
        /// If Directory already exists, deletes directory then creates an empty one
        /// </summary>
        /// <param name="directory">Path to directory to create</param>
        public static void DeleteThenCreate(this DirectoryInfo directory)
        {
            if (directory.Exists)
            {
                directory.Delete(true);
            }

            directory.Create();
        }

        /// <summary>
        /// Checks if directory contains all files in file names
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        public static bool DirectoryContains(this DirectoryInfo directory, params string[] fileNames)
        {
            if (!directory.Exists)
            {
                return false;
            }

            FileInfo[] directory_files = directory.GetFiles();
            IEnumerable<FileInfo> test_files = fileNames.Select(s => new FileInfo(Path.Combine(directory.FullName, s)));

            foreach (FileInfo file in test_files)
            {
                if (!directory_files.Contains(file))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Traverses all folders that are children of the directory info and
        /// does an action to all files found. This also does the action to 
        /// all files directly children of the directory info
        /// </summary>
        public static void HierachyDo_Files(this DirectoryInfo dir, Action<FileInfo> action)
        {
            Queue<DirectoryInfo> toProcess = new Queue<DirectoryInfo>();
            toProcess.Enqueue(dir);

            while(toProcess.Count > 0)
            {
                DirectoryInfo target = toProcess.Dequeue();
                foreach (FileInfo fi in target.EnumerateFiles()) action(fi);
                foreach (DirectoryInfo di in target.EnumerateDirectories()) toProcess.Enqueue(di);
            }
        }
        #endregion
    }
}