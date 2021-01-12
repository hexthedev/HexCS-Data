using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Represents an argument for a function. 
    /// This arg has a name, which gives it the name = value pattern
    /// </summary>
    public class Arg_Named : IArg
    {
        /// <summary>
        /// Parameter keywords
        /// </summary>
        public KeywordsCollection Keywords { get; private set; } = new KeywordsCollection();

        /// <summary>
        /// Name of paramter to apply arg to
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Value of the argument
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Arg with single value
        /// </summary>
        /// <param name="name">Name of paramter to apply arg</param>
        /// <param name="value">Value fo the arg</param>
        public Arg_Named(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Get this as a standard paramter string
        /// </summary>
        /// <returns></returns>
        public string ToArgumentString()
        {
            return $"{Keywords.ToKeywordString()}{Name} = {Value}";
        }
    }
}
