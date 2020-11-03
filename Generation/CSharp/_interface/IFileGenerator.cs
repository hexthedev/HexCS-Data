using System;
using System.Collections.Generic;
using System.Text;
using Hex.Paths;
using HexCS.Data.Persistence;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// A generator that allows the user to supply an xml comment
    /// </summary>
    public interface IFileGenerator : IGenerator
    {
        /// <summary>
        /// If true, the FileGenerator will not output to a file
        /// when Generate() is called
        /// </summary>
        bool SupressFileGeneration { get; }

        /// <summary>
        /// The path to the file that should be written to. 
        /// NOTE: the file will automatically be overriden if it exists
        /// </summary>
        PathString Path { get; }
    }
}
