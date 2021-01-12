using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Represents an argument for a function. 
    /// This is not a named argument
    /// </summary>
    public class Arg_Basic : IArg
    {
        /// <summary>
        /// Parameter keywords
        /// </summary>
        public KeywordsCollection Keywords { get; private set; } = new KeywordsCollection();

        /// <summary>
        /// Value of the argument
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Arg with single value
        /// </summary>
        /// <param name="value">Value of the arg</param>
        public Arg_Basic(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Get this as a standard paramter string
        /// </summary>
        /// <returns></returns>
        public string ToArgumentString()
        {
            return $"{Keywords.ToKeywordString()}{Value}";
        }
    }
}
