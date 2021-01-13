using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HexCS.Core;
using HexCS.Core;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Generates a CSharp function
    /// </summary>
    public class GTFile : AFileGenerator, INamespace
    {
        private List<INamespace> _namespaces = new List<INamespace>();
        private IUsings _usings;

        #region Public API
        /// <summary>
        /// Construct GTClass. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        /// <param name="path">path to the file to write to</param>
        /// <param name="encoding">encoding to write file in</param>
        public GTFile(StringBuilder output, PathString path = null, Encoding encoding = null) : base(output, path, encoding)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTFile() : base() { }

        /// <inheritdoc/>
        public override void Generate()
        {
            _usings?.Generate();

            OutputBuilder.AppendLine();
            OutputBuilder.AppendLine();

            OutputBuilder.AppendCharacterSeparatedCollection(
                _namespaces, (s, e) => e.Generate(), "\r\n\r\n"
            );

            if (!SupressFileGeneration)
            {
                PathString dir = Path.RemoveAtEnd();

                if (dir.TryAsDirectoryInfo(out DirectoryInfo dirInfo))
                {
                    Persistence.UTDirectoryInfo.ExistsOrCreate(dirInfo);
                    WriteToFile(OutputBuilder.ToString());
                } else
                {
                    throw new IOException($"Unable to get dir as DirectoryInfo: {dir}. file save failed");
                }
            }
        }

        /// <summary>
        /// returns a generator for a namespace
        /// </summary>
        /// <returns>Property Function Generator</returns>
        public T Generate_Namespace<T>() where T: INamespace, new()
        {
            T namespac = CreateInternalGenerator<T>();
            _namespaces.Add(namespac);
            return namespac;
        }

        /// <summary>
        /// returns a generator for file usings. Extra calls will override last value
        /// </summary>
        /// <returns>Property Function Generator</returns>
        public T Generate_Usings<T>() where T : IUsings, new()
        {
            T namespaceObject = CreateInternalGenerator<T>();
            _usings = namespaceObject;
            return namespaceObject;
        }
        #endregion
    }
}
