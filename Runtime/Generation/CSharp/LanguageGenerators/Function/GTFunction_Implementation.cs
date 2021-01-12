using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Generates a CSharp function with an implemenation
    /// </summary>
    public class GTFunction_Implementation : AGenericTypeGenerator, IFunction
    {
        private ParameterCollection _parameters = new ParameterCollection();
        private List<string> _statments = new List<string>();

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
        public GTFunction_Implementation(StringBuilder output) : base(output)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTFunction_Implementation() : base() { }

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

            OutputBuilder.AppendLine($"{IndentProvider.IndentString}{Keywords.ToKeywordString()}{ReturnType} {Name}({_parameters.ToFunctionInputString()})");
            GenerateWhereConstraints(this);
            OutputBuilder.AppendLine($"{IndentProvider.IndentString}{{");

            IndentProvider.Indent++;
            foreach (string statement in _statments)
            {
                OutputBuilder.AppendLine($"{IndentProvider.IndentString}{statement}");
            }
            IndentProvider.Indent--;

            OutputBuilder.Append($"{IndentProvider.IndentString}}}");
        }

        /// <summary>
        /// Adds paramaters to the function
        /// </summary>
        /// <param name="parameters"></param>
        public void Add_Parameters(params IParameterBlock[] parameters) => _parameters.AddParameters(parameters);

        /// <summary>
        /// Adds statements to the function
        /// </summary>
        /// <param name="statements"></param>
        public void Add_Statements(params string[] statements) => _statments.AddRange(statements);
        #endregion
    }
}
