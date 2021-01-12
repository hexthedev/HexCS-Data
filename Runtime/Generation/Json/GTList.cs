using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.Json
{
    /// <summary>
    /// Generates a Json list
    /// </summary>
    public class GTList : AGenerator
    {
        /// <summary>
        /// The object to generate
        /// </summary>
        public ICollection List;

        /// <summary>
        /// Construct a GTList to take some primative as an object and 
        /// </summary>
        /// <param name="sb"></param>
        public GTList(StringBuilder sb) : base(sb) { }

        /// <summary>
        /// Empty for internal purposes
        /// </summary>
        public GTList() : base() { }

        /// <summary>
        /// Set required fields
        /// </summary>
        public void SetRequired(List<object> list)
        {
            List = list;
        }

        /// <inheritdoc/>
        public override void Generate()
        {
            GTPrimative p = CreateInternalGenerator<GTPrimative>();

            OutputBuilder.AppendLine($"[");
            IndentProvider.Indent++;

            foreach (object o in List)
            {
                p.Primative = o;
                OutputBuilder.Append($"{IndentProvider.IndentString}");
                p.Generate();
                OutputBuilder.AppendLine(",");
            }

            IndentProvider.Indent--;
            OutputBuilder.Append($"{IndentProvider.IndentString}]");
        }
    }
}
