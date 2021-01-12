using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Generates a CSharp enum
    /// </summary>
    public class GTEnum : ACommonCSharpGenerator, IEnum, INamespaceObject
    {
        private List<EnumValue> _values  = new List<EnumValue>();

        #region API
        /// <summary>
        /// Name of the enum
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Construct GTClass. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public GTEnum(StringBuilder output) : base(output)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTEnum() : base() { }

        /// <summary>
        /// Set the required parameters for this generator
        /// </summary>
        public void SetRequired(string name, EnumValue[] values, params EKeyword[] keywords)
        {
            Add_Keywords(keywords);
            Name = name;
            Add_EnumValues(values);
        }

        /// <inheritdoc/>
        public override void Generate()
        {
            GenerateComment();
            GenerateAttributes();

            OutputBuilder.AppendLine($"{IndentProvider.IndentString}{Keywords.ToKeywordString()}enum {Name} {{");

            IndentProvider.Indent++;
            for(int i = 0; i < _values.Count; i++)
            {
                OutputBuilder.Append($"{IndentProvider.IndentString}{_values[i].ToEnumValueString()}");
                if (i != _values.Count - 1) OutputBuilder.Append(",");
                OutputBuilder.AppendLine();
            }
            IndentProvider.Indent--;

            OutputBuilder.Append($"{IndentProvider.IndentString}}}");
        }

        /// <summary>
        /// Add an enum value to the enum
        /// </summary>
        /// <param name="enumValues"></param>
        public void Add_EnumValues(params EnumValue[] enumValues) => _values.AddRange(enumValues);
        #endregion
    }
}
