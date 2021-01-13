using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HexCS.Core;
using HexCS.Data.Persistence;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// A generator that automatically generate the output to a file when
    /// Generate is called (unless to surpress file generation flag is true)
    /// </summary>
    public abstract class AFileGenerator : AGenerator, IFileGenerator
    {
        #region Internal API 
        /// <summary>
        /// Empty generator. Note that empty generators will not function correctly. The
        /// empty constructor is used internally.
        /// </summary>
        internal AFileGenerator() : base() { }
        #endregion

        #region Protected API
        /// <summary>
        /// Writes the generated string to a file if the Path property of 
        /// this class is valid. Note, this will override ecisting files.
        /// </summary>
        /// <returns></returns>
        protected bool WriteToFile(string content)
        {
            if(Path == null || !Path.TryAsFileInfo(out FileInfo info)) return false;
            info.ForceEmptyOrCreate();

            try
            {
                info.WriteString(content, Encoding);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region Public API
        /// <inheritdoc />
        public bool SupressFileGeneration { get; set; } = false;
        
        /// <inheritdoc />
        public PathString Path { get; private set; }

        /// <inheritdoc />
        public Encoding Encoding { get; private set; } = Encoding.UTF8;

        /// <summary>
        /// Construct ACommentableGenerator. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        /// <param name="path">output path, null path will not write to path</param>
        /// <param name="encoding">encoding, null will default to UTF8</param>
        public AFileGenerator(StringBuilder output, PathString path = null, Encoding encoding = null) : base(output)
        {
            Path = path;
            if (Encoding != null) Encoding = encoding;
        }
        #endregion
    }
}
