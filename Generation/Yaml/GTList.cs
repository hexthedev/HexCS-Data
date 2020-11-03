using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HexCS.Data.Runtime;

namespace HexCS.Data.Generation.Yaml
{
    /// <summary>
    /// Generates a YAML list
    /// </summary>
    public class GTList : AGenerator
    {
        /// <summary>
        /// The object to generate
        /// </summary>
        public InterField List;

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
        public void SetRequired(InterField list)
        {
            List = list;
        }

        /// <inheritdoc/>
        public override void Generate()
        {
            GTInterField o = CreateInternalGenerator<GTInterField>();
            o.FieldIsListElement = true;

            InterObject inter = (InterObject)List.Object; // Fields are list elements

            if(List.Type == EDataType.Data)
            {
                foreach (InterField f in inter.Fields)
                {
                    OutputBuilder.AppendLine($"{IndentProvider.IndentString}- {f.Name}:");

                    o.Field = f;
                    IndentProvider.Indent++;
                    o.Generate();
                    IndentProvider.Indent--;
                }
            }
            else
            {
                foreach (InterField f in inter.Fields)
                {
                    OutputBuilder.Append($"{IndentProvider.IndentString}- ");

                    o.Field = f;
                    o.Generate();
                }
            }
        }
    }
}
