using System;
using System.IO;
using System.Linq;
using HexCS.Core;

namespace HexCS.Data.Persistence
{
    /// <summary>
    /// TTypeEnum enumerate all types present in library
    /// </summary>
    /// <typeparam name="TFileTypeEnum"></typeparam>
    public class DataLibrary<TFileTypeEnum>
    {
        private PathString _rootps;
        private DirectoryInfo _root;

        private TFileTypeEnum[] _keys = UTEnum.GetEnumAsArray<TFileTypeEnum>();

        private EnumDicitonary<TFileTypeEnum, PathString> _folderPaths = new EnumDicitonary<TFileTypeEnum, PathString>();
        private EnumDicitonary<TFileTypeEnum, DirectoryInfo> _folderDirectoryInfos = new EnumDicitonary<TFileTypeEnum, DirectoryInfo>();
        private EnumDicitonary<TFileTypeEnum, PathString[]> _files = new EnumDicitonary<TFileTypeEnum, PathString[]>();

        /// <summary>
        /// The Root library folder
        /// </summary>
        public PathString Root
        {
            get => _rootps;
            private set
            {
                _rootps = value;

                if (!Root.TryAsDirectoryInfo(out _root))
                {
                    throw new ArgumentException("Root is not valid directory path");
                }

                foreach(TFileTypeEnum e in _keys)
                {
                    _folderPaths[e] = _rootps + e.ToString();
                }
            }
        }

        /// <summary>
        /// Make a data library wiht some root
        /// </summary>
        /// <param name="root"></param>
        public DataLibrary(PathString root)
        {
            Root = root;
        }

        /// <summary>
        /// Checks the Root folder, makes sure folders exist for each enum
        /// and populates file pointers.
        /// </summary>
        public void RefreshFolders()
        {
            foreach(TFileTypeEnum ps in _keys)
            {
                if(!_folderPaths[ps].TryAsDirectoryInfo(out DirectoryInfo di))
                {
                    throw new ArgumentException($"{_folderPaths} cannot be created as DirectoryInfo");
                }

                di.ExistsOrCreate();
                _folderDirectoryInfos[ps] = di;
            }
        }

        /// <summary>
        /// Refresh all file types in library
        /// </summary>
        public void RefreshAllFiles()
        {
            foreach(TFileTypeEnum e in _keys)
            {
                RefreshFiles(e);
            }
        }

        /// <summary>
        /// Refreshs the files in the libary
        /// </summary>
        /// <param name="fileType"></param>
        public void RefreshFiles(TFileTypeEnum fileType)
        {
            DirectoryInfo di = _folderDirectoryInfos[fileType];
            _files[fileType] = di.GetFiles().Select(f => new PathString(f.FullName)).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="autoRefresh"></param>
        /// <returns></returns>
        public PathString[] GetFiles(TFileTypeEnum fileType, bool autoRefresh = true)
        {
            if (autoRefresh) RefreshFiles(fileType);
            return Core.UTArray.ShallowCopy(_files[fileType]);
        }

        /// <summary>
        /// Writes a file to the dataLibrary
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="data"></param>
        public void WriteFile(byte[] data, TFileTypeEnum fileType)
        {
            PathString folder = _folderPaths[fileType];

            PathString file = folder.InsertAtEnd($"{UTRandom.Int(int.MaxValue).ToString()}.data");
            if (!file.TryAsFileInfo(out FileInfo info)) throw new IOException($"Failed to write file {file}");

            info.ForceEmptyOrCreate();
            info.WriteBytes(data);
        }

        /// <summary>
        /// Removes all files from folders
        /// </summary>
        public void Clear()
        {
            foreach(TFileTypeEnum e in _keys)
            {
                _folderDirectoryInfos[e].DeleteThenCreate();
            }
        }
    }
}
