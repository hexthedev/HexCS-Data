using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// A definition for a function that should appear in an interface
    /// </summary>
    public class GTInterfaceDefinition_Function : ACommentableGenerator, IInterfaceDefinition
    {
        private ParameterCollection _parameters { get; } = new ParameterCollection();

        #region API
        /// <summary>
        /// The function definition return type
        /// </summary>
        public string ReturnType;

        /// <summary>
        /// The funciton definition name
        /// </summary>
        public string Name;


        /// <summary>
        /// Construct GTFunction. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public GTInterfaceDefinition_Function(StringBuilder output) : base(output)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTInterfaceDefinition_Function() : base() { }

        /// <summary>
        /// Set the required parameters for this generator
        /// </summary>
        public void SetRequired(string returnType, string name)
        {
            Name = name;
            ReturnType = returnType;
        }

        /// <inheritdoc />
        public override void Generate()
        {
            GenerateComment();

            OutputBuilder.Append($"{IndentProvider.IndentString}{ReturnType} {Name}({_parameters.ToFunctionInputString()});");
        }

        /// <summary>
        /// Add Paramaters tot the Interface Definition
        /// </summary>
        /// <param name="parameters"></param>
        public void Add_Paramaters(params IParameterBlock[] parameters) => _parameters.AddParameters(parameters);
        #endregion
    }
}
