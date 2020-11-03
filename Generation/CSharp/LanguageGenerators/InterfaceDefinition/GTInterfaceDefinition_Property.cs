using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// A definition for a function that should appear in an interface
    /// </summary>
    public class GTInterfaceDefinition_Property : ACommentableGenerator, IInterfaceDefinition
    {
        #region API
        /// <summary>
        /// Does this definition contain a get function
        /// </summary>
        public bool Get;

        /// <summary>
        /// Does this definition contain a set function
        /// </summary>
        public bool Set;

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
        public GTInterfaceDefinition_Property(StringBuilder output) : base(output)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTInterfaceDefinition_Property() : base() { }

        /// <summary>
        /// Set the required parameters for this generator
        /// </summary>
        public void SetRequired(string returnType, string name, bool get, bool set)
        {
            Name = name;
            ReturnType = returnType;
            Get = get;
            Set = set;
        }

        /// <inheritdoc />
        public override void Generate()
        {
            GenerateComment();

            string getSet = " ";

            if(Get && Set)
            {
                getSet = " get; set; ";
            } else if(Set)
            {
                getSet = " set; ";
            } else if(Get)
            {
                getSet = " get; ";
            }

            OutputBuilder.Append($"{IndentProvider.IndentString}{ReturnType} {Name} {{{getSet}}}");
        }
        #endregion
    }
}
