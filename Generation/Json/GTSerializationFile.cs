using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.Json
{
    /// <summary>
    /// Generates a Json file starting with %Json 1.2 and enclosing file in
    /// ---, ...
    /// </summary>
    public class GTSerializationFile : AGenerator
    {
        /// <summary>
        /// The objects to write to the Json file
        /// </summary>
        public KeyValuePair<string, object>[] Objects;

        /// <summary>
        /// Construct generator
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="objects"></param>
        public GTSerializationFile(StringBuilder sb, KeyValuePair<string, object>[] objects) : base(sb)
        {
            Objects = objects;
        }

        /// <summary>
        /// Empty, for internal purposes
        /// </summary>
        public GTSerializationFile() { }

        /// <inheritdoc/>
        public override void Generate()
        {
            OutputBuilder.AppendLine("{");
            IndentProvider.Indent++;

            using (GTObject gto = CreateInternalGenerator<GTObject>())
            {
                foreach(KeyValuePair<string, object> obj in Objects)
                {
                    gto.Obj = obj;
                    gto.Generate();
                    OutputBuilder.AppendLine(",");
                }
            }

            OutputBuilder.Append("{");
        }
    }
}
