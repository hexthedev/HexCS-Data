using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HexCS.Data.Runtime;

namespace HexCS.Data.Generation.Json
{
    /// <summary>
    /// Generates a Json list
    /// </summary>
    public class GTObject : AGenerator
    {
        /// <summary>
        /// The object to generate
        /// </summary>
        public KeyValuePair<string, object> Obj;

        /// <summary>
        /// Construct a GTObject to take some primative as an object and 
        /// </summary>
        /// <param name="sb"></param>
        public GTObject(StringBuilder sb) : base(sb) { }

        /// <summary>
        /// Empty for internal purposes
        /// </summary>
        public GTObject() : base() { }

        /// <summary>
        /// Set required fields
        /// </summary>
        public void SetRequired(KeyValuePair<string, object> obj)
        {
            Obj = obj;
        }

        /// <inheritdoc/>
        public override void Generate()
        {
            OutputBuilder.Append($"{IndentProvider.IndentString}{Obj.Key}:");

            switch (Obj.Value)
            {
                //case IData d:
                //    OutputBuilder.AppendLine(" {");
                //    IndentProvider.Indent++;
                //    GTObject gto1 = CreateInternalGenerator<GTObject>();
                //    foreach (KeyValuePair<string, object> kv in d.ConvertToIntermediate())
                //    {
                //        gto1.Obj = kv;
                //        gto1.Generate();
                //    }
                //    OutputBuilder.AppendLine();
                //    IndentProvider.Indent--;
                //    OutputBuilder.AppendLine($"{IndentProvider.IndentString}}}");
                //    break;

                case KeyValuePair<string, object> o:
                    IndentProvider.Indent++;
                    GTObject gto2 = CreateInternalGenerator<GTObject>();
                    gto2.Obj = o;
                    gto2.Generate();
                    IndentProvider.Indent--;
                    OutputBuilder.AppendLine();
                    break;

                case ICollection l:
                    OutputBuilder.Append(" ");
                    GTList gtl = CreateInternalGenerator<GTList>();
                    gtl.List = l;
                    gtl.Generate();
                    break;

                default:
                    OutputBuilder.Append(" ");
                    GTPrimative gtp = CreateInternalGenerator<GTPrimative>();                    
                    gtp.Primative = Obj.Value;
                    gtp.Generate();
                    break;
            }
        }
    }
}
