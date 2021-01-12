using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Used to generate xml comments in CSharp code
    /// </summary>
    public class GTComment_Inheritdoc : AGenerator, IComment
    {
        #region API
        /// <summary>
        /// Construct GTComment_Inheritdoc. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public GTComment_Inheritdoc(StringBuilder output) : base(output)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTComment_Inheritdoc() : base() { }

        /// <inheritdoc />
        public override void Generate() => OutputBuilder.AppendLine($"{IndentProvider.IndentString}/// <inheritdoc />");
        #endregion
    }
}
