using System.Text;
using HexCS.Data.Runtime;

namespace HexCS.Data.Generation.Yaml
{
    /// <summary>
    /// Generates a Yaml file starting with %YAML 1.2 and enclosing file in
    /// ---, ...
    /// </summary>
    public class GTIDataFile : AGenerator
    {
        private const string cYamlHeader = "%YAML 1.2";
        private const string cYamlStartFile = "---";
        private const string cYamlEndFile = "***";

        /// <summary>
        /// The objects to write to the YAML file
        /// </summary>
        public IData Data;

        /// <summary>
        /// Construct generator
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="obj"></param>
        public GTIDataFile(StringBuilder sb, IData obj) : base(sb)
        {
            Data = obj;
            IndentProvider.IndentCharacter = "  "; // set to two space indent character
        }

        /// <summary>
        /// Empty, for internal purposes
        /// </summary>
        public GTIDataFile() { }

        /// <inheritdoc/>
        public override void Generate()
        {
            InterObject Obj = Data.ConvertToIntermediate();

            OutputBuilder.AppendLine(cYamlHeader);
            OutputBuilder.AppendLine(cYamlStartFile);
            
            using (GTInterField gto = CreateInternalGenerator<GTInterField>())
            {
                gto.AppendNewLine = true;

                foreach(InterField obj in Obj.Fields)
                {
                    gto.Field = obj;
                    gto.Generate();
                }
            }

            OutputBuilder.Append(cYamlEndFile);
        }
    }
}
