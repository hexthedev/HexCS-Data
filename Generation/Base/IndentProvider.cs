using System;
using System.Collections.Generic;
using System.Text;
using HexCS.Core;

namespace HexCS.Data.Generation
{
    /// <summary>
    /// Provides an indent value (int)
    /// </summary>
    public class IndentProvider
    {
        /// <summary>
        /// The indent value. Number of tabs to apply
        /// </summary>
        public int Indent { get; set; }

        /// <summary>
        /// The character used to indent the generated line
        /// </summary>
        public string IndentCharacter = "   ";

        /// <summary>
        /// Set this to true if indent calculatation should ignore indent
        /// </summary>
        public bool IgnoreIndent = false;

        /// <summary>
        /// Returns a string repeating 3 spaces (tabs converted to 3 spaces) based on indent
        /// </summary>
        public string IndentString => IgnoreIndent ? string.Empty : UTString.RepeatedCharacter(IndentCharacter, Indent);

        /// <summary>
        /// Constrcut an indent provided
        /// </summary>
        /// <param name="indent">initial indent</param>
        public IndentProvider(int indent = 0)
        {
            Indent = indent;
        }
    }
}
