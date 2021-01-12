using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static HexCS.Data.Parsing.TypeNameParser;

namespace HexCS.Data.Parsing
{
    /// <summary>
    /// Takes a string of pattern [TYPE] NAME and outputs a TypeName object
    /// </summary>
    public class TypeNameParser : IParser<TypeName>
    {
        /// <summary>
        /// string given to invalid parse units
        /// </summary>
        public const string cInvalid = "PARSE_FAIL";

        /// <inheritdoc />
        public TypeName Interpret(string input)
        {
            int bracket1 = input.IndexOf('[');
            int bracket2 = input.IndexOf(']');

            //Possibilites
            // Calulation is B2 - B1 - 2.
            // Need to check if B1 == -1 still, but if B2 == -1 then typeLength will be negative or 0
            // if the [] input is invalid
            int typeLength = bracket2 - bracket1 - 1;
            string type = bracket1 == -1 || typeLength <= 0 ? cInvalid : input.Substring(bracket1 + 1, typeLength);


            int lengthAfterType = input.Length - bracket2;
            string name = bracket2 == -1 || lengthAfterType <= 0 ? cInvalid : input.Substring(bracket2 + 1).Trim();

            return new TypeName() { Type = type, Name = name };
        }

        /// <summary>
        /// A type and a name
        /// </summary>
        public struct TypeName
        {
            /// <summary>
            /// The type
            /// </summary>
            public string Type;

            /// <summary>
            /// The Name
            /// </summary>
            public string Name;
        }
    }
}
