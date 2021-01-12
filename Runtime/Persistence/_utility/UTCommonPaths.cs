using Hex.Paths;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HexCS.Data.Persistence
{
    /// <summary>
    /// Utility that contains useful paths used for testing
    /// </summary>
    public static class UTCommonPaths
    {
        private static Lazy<PathString> _appDataPath = new Lazy<PathString>(CreateAppDataPath);

        #region Public API
        /// <summary>
        /// Absolute path to the location on the folder on this PC
        /// where application data can be used. This is useful for 
        /// many things, like caching app data between sessinos or making
        /// tempory folders for testing purposes.
        /// 
        /// Note: null if something fails in path resolution
        /// </summary>
        public static PathString AppDataPath = _appDataPath.Value;
        #endregion

        private static PathString CreateAppDataPath()
        {
            try
            {
                return new PathString(Environment.GetEnvironmentVariable("APPDATA"));
            } catch (Exception e)
            {
                return null;
            }
        }
    }
}
