using System;
using System.Collections.Generic;
using System.Text;
using HexCS.Core;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Generates a property in a class that uses get only syntax. Name => Value;
    /// 
    /// Note: End of generation does not apply new line
    /// </summary>
    public class GTProperty_GetOnly : ACommonCSharpGenerator, IProperty
    {
        private IValue _defaultValue = null;

        /// <summary>
        /// The type of the field
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// The name of the field
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Construct GTProperty. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public GTProperty_GetOnly(StringBuilder output) : base(output)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTProperty_GetOnly() : base() { }

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

            OutputBuilder.Append($"{IndentProvider.IndentString}{Keywords.ToKeywordString()}{Type} {Name} => ");

            if (_defaultValue != null)
            {
                _defaultValue.Generate();
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
            _defaultValue = val;
            return val;
        }
    }
}
