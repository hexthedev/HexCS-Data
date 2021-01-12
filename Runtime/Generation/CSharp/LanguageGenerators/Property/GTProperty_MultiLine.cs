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
    public class GTProperty_MultiLine : ACommonCSharpGenerator, IProperty
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
        public GTProperty_MultiLine(StringBuilder output) : base(output) { }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTProperty_MultiLine() : base() { }

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

            OutputBuilder.AppendLine($"{IndentProvider.IndentString}{Keywords.ToKeywordString()}{Type} {Name} {{");

            IndentProvider.Indent++;
            if (GetFunction.IsPresent)
            {
                GenerateFunction(GetFunction, "get", OutputBuilder);
                GenerateFunction(SetFunction, "set", OutputBuilder);
            }
            IndentProvider.Indent--;
            OutputBuilder.Append($"{IndentProvider.IndentString}}}");

            if (DefaultValue != null)
            {
                OutputBuilder.Append(" = ");
                DefaultValue.Generate();
                OutputBuilder.Append(";");
            }
        }

        private void GenerateFunction(FunctionParams function, string functionName, StringBuilder builder)
        {
            if(!function.IsPresent) return;

            if (function.Statements != null)
            {
                builder.AppendLine($"{IndentProvider.IndentString}{function.Keywords?.ToKeywordString()}{functionName} {{");

                IndentProvider.Indent++;
                foreach (string stat in function.Statements)
                {
                    builder.AppendLine($"{IndentProvider.IndentString}{stat}");
                }
                IndentProvider.Indent--;

                builder.AppendLine($"{IndentProvider.IndentString}}}");
            } else
            {
                builder.AppendLine($"{IndentProvider.IndentString}{function.Keywords?.ToKeywordString()}{functionName};");
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

        /// <summary>
        /// Parameters describing the state of a property function
        /// </summary>
        public struct FunctionParams
        {
            /// <summary>
            /// Is this function present
            /// </summary>
            public bool IsPresent { get; set; }

            /// <summary>
            /// What are the statements in the function
            /// </summary>
            public string[] Statements { get; set; }

            /// <summary>
            /// Keywords in front of the property
            /// </summary>
            public KeywordsCollection Keywords { get; set; }
        }
    }
}
