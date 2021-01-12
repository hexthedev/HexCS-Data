using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Parsing.Parsers.Yaml
{
    /// <summary>
    /// Parses a single YamlObject, expecting that all provided rows are a
    /// trimed yaml object. 
    /// </summary>
    public class RowParserYamlObject : IRowParser<KeyValuePair<string, object>>
    {
        /// <inheritdoc />
        public KeyValuePair<string, object> Interpret(string[] input)
        {
            if (input == null || input.Length == 0) return default;
            string headLine = input[0];
            int colonIndex = headLine.IndexOf(':');
            if (colonIndex < 1) return default;

            // one liner object
            if (input.Length == 1)
            {
                string[] obj = headLine.Split(':');
                if (obj.Length != 2) return default;
                return new KeyValuePair<string, object>(obj[0], obj[1]);
            }

            return default;
        }
    }
}
