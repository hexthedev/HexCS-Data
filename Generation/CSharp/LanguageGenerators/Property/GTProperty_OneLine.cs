using System;
using System.Collections.Generic;
using System.Text;
using HexCS.Core;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Generates a property in a class
    /// 
    /// Note: End of generation does not apply new line
    /// </summary>
    public class GTProperty_OneLine : ACommonCSharpGenerator, IProperty
    {
        /// <summary>
        /// The type of the field
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// The name of the field
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Get function paramters
        /// </summary>
        public FunctionParams GetFunction { get; set; }

        /// <summary>
        /// Set function paramters
        /// </summary>
        public FunctionParams SetFunction { get; set; }

        /// <summary>
        /// Does this filed have a default value
        /// </summary>
        public IValue DefaultValue = null;

        /// <summary>
        /// Construct GTProperty. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public GTProperty_OneLine(StringBuilder output) : base(output)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTProperty_OneLine() : base() { }

        /// <summary>
        /// Set the required parameters for this generator
        /// </summary>
        public void SetRequired(string type, string name, params EKeyword[] keywords)
        {
            Type = type;
            Name = name;
            Add_Keywords(keywords);
        }

        /// <inheritdoc />
        public override void Generate()
        {
            // genreate the comment
            GenerateComment();
            GenerateAttributes();

            OutputBuilder.Append($"{IndentProvider.IndentString}{Keywords.ToKeywordString()}{Type} {Name} {{ ");

            if (GetFunction.IsPresent)
            {
                string get = GenerateFunction(GetFunction, "get");
                string set = GenerateFunction(SetFunction, "set");

                if (!string.IsNullOrEmpty(get))
                {
                    OutputBuilder.Append($"{get}");
                }

                OutputBuilder.Append(" ");

                if (!string.IsNullOrEmpty(set))
                {
                    OutputBuilder.Append($"{set} ");
                }

                OutputBuilder.Append("}");
            }

            if (DefaultValue != null)
            {
                OutputBuilder.Append(" = ");
                DefaultValue.Generate();
                OutputBuilder.Append(";");
            }
        }

        /// <summary>
        /// returns a generator for a field
        /// </summary>
        /// <returns>Property Function Generator</returns>
        public T Generate_DefaultValue<T>() where T : IValue, new()
        {
            T val = CreateInternalGenerator<T>();
            DefaultValue = val;
            return val;
        }

        private string GenerateFunction(FunctionParams function, string functionName)
        {
            if(!function.IsPresent) return string.Empty;

            if (!string.IsNullOrEmpty(function.Statement))
            {
                return $"{function.Keywords?.ToKeywordString()}{functionName} => {function.Statement}";
            } else
            {
                return $"{function.Keywords?.ToKeywordString()}{functionName};";
            }
        }


        /// <summary>
        /// Parameters describing the state of a property function
        /// </summary>
        public struct FunctionParams
        {
            /// <summary>
            /// Keywords in front of the property
            /// </summary>
            public bool IsPresent { get; set; }

            /// <summary>
            /// Keywords in front of the property
            /// </summary>
            public string Statement { get; set; }

            /// <summary>
            /// Keywords in front of the property
            /// </summary>
            public KeywordsCollection Keywords { get; set; }
        }
    }
}
