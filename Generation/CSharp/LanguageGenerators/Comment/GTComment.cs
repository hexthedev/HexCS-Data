using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Used to generate xml comments in CSharp code
    /// </summary>
    public class GTComment : AGenerator, IComment
    {
        private List<Parameter> _parameters = new List<Parameter>();

        #region API
        /// <summary>
        /// Comment summary
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Comment return details
        /// </summary>
        public string Returns { get; set; }

        /// <summary>
        /// Construct GTComment. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public GTComment(StringBuilder output) : base(output)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTComment() : base() { }

        /// <summary>
        /// Set the required parameters for this generator
        /// </summary>
        public void SetRequired(string summary)
        {
            Summary = summary;
        }

        /// <inheritdoc />
        public override void Generate()
        {
            OutputBuilder.AppendLine($"{IndentProvider.IndentString}/// <summary>");
            OutputBuilder.AppendLine($"{IndentProvider.IndentString}/// {Summary}");
            OutputBuilder.AppendLine($"{IndentProvider.IndentString}/// </summary>");
            
            foreach(Parameter p in _parameters)
            {
                OutputBuilder.AppendLine($"{IndentProvider.IndentString}{p.ToGeneratorString()}");
            }

            if (!string.IsNullOrEmpty(Returns))
            {
                OutputBuilder.Append($"{IndentProvider.IndentString}/// <returns>{Returns}</returns>");
            }
        }

        /// <summary>
        /// Add a paramter to the comments
        /// </summary>
        /// <param name="paramater">paramter to add</param>
        public void Add_Paramater(Parameter paramater)
        {
            _parameters.Add(paramater);
        }
        #endregion

        /// <summary>
        /// Variables required to generate "~param name="NAME"~ DETAILS ~/param~" string
        /// </summary>
        public struct Parameter
        {
            /// <summary>
            /// Name of the parameter
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Details explaining paramater
            /// </summary>
            public string Details { get; set; }

            /// <summary>
            /// Create comment parameter
            /// </summary>
            /// <param name="name">name of the paramter</param>
            /// <param name="details">deatils explaining the paramters</param>
            public Parameter(string name, string details)
            {
                Name = name;
                Details = details;
            }

            /// <summary>
            /// Output as generated string for AGenerator classes
            /// </summary>
            /// <returns>generated string in xml format</returns>
            public string ToGeneratorString()
            {
                string name = string.IsNullOrEmpty(Name) ? "MISSING_PARAM_NAME" : Name;
                string details = string.IsNullOrEmpty(Details) ? "" : Details;

                return $"/// <param name=\"{ name }\">{details}</param>";
            }
        }
    }
}
