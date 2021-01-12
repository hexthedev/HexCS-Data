using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Used to generate attributes in c#
    /// </summary>
    public class GTAttribute : AGenerator, IAttribute
    {
        private ArgCollection _args = new ArgCollection();

        #region API
        /// <summary>
        /// Name of the attribute
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Construct GTAttribute. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public GTAttribute(StringBuilder output) : base(output)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTAttribute() : base() { }

        /// <summary>
        /// Set the required parameters for this generator
        /// </summary>
        public void SetRequired(string name)
        {
            Name = name;
        }

        /// <inheritdoc />
        public override void Generate()
        {
            if(_args.Count == 0)
            {
                OutputBuilder.Append($"{IndentProvider.IndentString}[{Name}]");
            }
            else
            {
                OutputBuilder.Append($"{IndentProvider.IndentString}[{Name}({_args.ToArgString()})]");
            }
        }

        /// <summary>
        /// Add args to the attribute
        /// </summary>
        /// <param name="args"></param>
        public void Add_Args(params IArg[] args) => _args.AddArgs(args);
        #endregion
    }
}
