using System.Collections.Generic;
using System.Text;
using HexCS.Core;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Generates usings for CSharp files.
    /// 
    /// Note: End of generation applies new line
    /// </summary>
    public class GTUsings : AGenerator, IUsings
    {
        private List<string> _Namespaces = new List<string>();

        /// <summary>
        /// Construct GTUsings. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public GTUsings(StringBuilder output) : base(output)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTUsings() : base() { }

        /// <summary>
        /// Set required parameters for this generator
        /// </summary>
        /// <param name="usings"></param>
        public void SetRequired(params string[] usings) => Add_Usings(usings);

        /// <summary>
        /// Add a using with the provided namespace
        /// </summary>
        /// <param name="namesp">namespace to add</param>
        public void Add_Usings(params string[] namesp) => _Namespaces.AddRange(namesp);

        /// <inheritdoc />
        public override void Generate()
        {
            OutputBuilder.AppendCharacterSeparatedCollection(
                _Namespaces, (s,e) => OutputBuilder.Append($"{IndentProvider.IndentString}using {e};"), "\r\n"
            );
        }
    }
}
