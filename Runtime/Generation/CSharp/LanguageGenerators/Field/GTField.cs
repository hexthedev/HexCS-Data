using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Generates a Csharp field
    /// 
    /// Note: End of generation applies new line
    /// </summary>
    public class GTField : ACommonCSharpGenerator, IField
    {
        private IValue _defaultValue = null;

        #region API
        /// <summary>
        /// The type of the field
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// The name of the field
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Construct GTField. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public GTField(StringBuilder output) : base(output)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTField() : base() { }

        /// <summary>
        /// Set the required parameters for this generator
        /// </summary>
        public void SetRequired(string type, string name, params EKeyword[] keywords)
        {
            Type = type;
            Name = name;
            Add_Keywords(keywords);
        }

        /// <inheritdoc/>
        public override void Generate()
        {
            // Comment
            GenerateComment();
            GenerateAttributes();

            // Field
            string name = string.IsNullOrEmpty(Name) ? "ERROR_MISSING_NAME" : Name;
            string type = string.IsNullOrEmpty(Type) ? "ERROR_MISSING_TYPE" : Type;

            OutputBuilder.Append($"{IndentProvider.IndentString}{Keywords.ToKeywordString()}{type} {name}");

            if(_defaultValue != null)
            {
                OutputBuilder.Append($" = ");
                _defaultValue.Generate();
            }

            OutputBuilder.Append(";");
        }

        /// <summary>
        /// Generate a DefaultValue for this field
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Generate_DefaultValue<T>() where T : IValue, new()
        {
            T valGenerator = CreateInternalGenerator<T>();
            _defaultValue = valGenerator;
            return valGenerator;
        }
        #endregion
    }
}