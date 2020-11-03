using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Parsing
{
    /// <summary>
    /// Takes a string a trims it
    /// </summary>
    public class TrimParser : IParser<string>
    {
        /// </inheritdoc>
        public string Interpret(string input) => input.Trim();
    }
}
