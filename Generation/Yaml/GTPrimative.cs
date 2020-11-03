using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.Yaml
{
    /// <summary>
    /// Generates a YAML primative
    /// </summary>
    public class GTPrimative : AGenerator
    {
        /// <summary>
        /// The object to generate
        /// </summary>
        public object Primative;

        /// <summary>
        /// Construct a GTPrimative to take some primative as an object and 
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="primative"></param>
        public GTPrimative(StringBuilder sb, object primative) : base(sb)
        {
            Primative = primative;
        }

        /// <summary>
        /// Empty for internal purposes
        /// </summary>
        public GTPrimative() : base() { }

        /// <inheritdoc/>
        public override void Generate()
        {
            OutputBuilder.AppendLine($"{Primative}");
        }
    }
}
