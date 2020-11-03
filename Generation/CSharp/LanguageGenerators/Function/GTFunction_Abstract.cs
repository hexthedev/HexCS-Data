using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Generates a CSharp abstract function
    /// </summary>
    public class GTFunction_Abstract : AGenericTypeGenerator, IFunction
    {
        private ParameterCollection _parameters = new ParameterCollection();

        #region Public API
        /// <summary>
        /// Return type of the function
        /// </summary>
        public string ReturnType { get; set; }

        /// <summary>
        /// Name of the function
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Construct GTFunction. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public GTFunction_Abstract(StringBuilder output) : base(output)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTFunction_Abstract() : base() { }

        /// <summary>
        /// Set the required parameters for this generator
        /// </summary>
        public void SetRequired(string returnType, string name, params EKeyword[] keywords)
        {
            Name = name;
            ReturnType = returnType;
            Add_Keywords(keywords);
        }

        /// <inheritdoc/>
        public override void Generate()
        {
            GenerateComment();
            GenerateAttributes();

            OutputBuilder.Append($"{IndentProvider.IndentString}{Keywords.ToKeywordString()}abstract {ReturnType} {Name}({_parameters.ToFunctionInputString()})");
            GenerateWhereConstraints(this);
            OutputBuilder.Append(";");
        }

        /// <summary>
        /// Adds paramaters to the function
        /// </summary>
        /// <param name="parameters"></param>
        public void Add_Paramaters(params IParameterBlock[] parameters) => _parameters.AddParameters(parameters);
        #endregion
    }
}
