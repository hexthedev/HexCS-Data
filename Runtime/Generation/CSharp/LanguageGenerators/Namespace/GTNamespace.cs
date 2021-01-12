using System;
using System.Collections.Generic;
using System.Text;
using HexCS.Core;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Generates a CSharp namespace
    /// </summary>
    public class GTNamespace : ACommentableGenerator, INamespace
    {
        private List<INamespaceObject> _objects = new List<INamespaceObject>();

        #region Public API
        /// <summary>
        /// Name of the class
        /// </summary>
        public string NameSpace { get; set; }

        /// <summary>
        /// Construct GTClass. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public GTNamespace(StringBuilder output) : base(output)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTNamespace() : base() { }

        /// <summary>
        /// Set the required parameters for this generator
        /// </summary>
        public void SetRequired(string namesp)
        {
            NameSpace = namesp;
        }

        /// <inheritdoc/>
        public override void Generate()
        {
            GenerateComment();

            OutputBuilder.AppendLine($"{IndentProvider.IndentString}namespace {NameSpace}");
            OutputBuilder.AppendLine("{");

            IndentProvider.Indent++;
            OutputBuilder.AppendCharacterSeparatedCollection(
                _objects, (s,e) => e.Generate(), "\r\n\r\n"
            );
            IndentProvider.Indent--;

            OutputBuilder.AppendLine();
            OutputBuilder.Append($"{IndentProvider.IndentString}}}");
        }

        /// <summary>
        /// returns a generator for a field
        /// </summary>
        /// <returns>Property Function Generator</returns>
        public T Generate_NamespaceObject<T>() where T: INamespaceObject, new()
        {
            T namespaceObject = CreateInternalGenerator<T>();
            _objects.Add(namespaceObject);
            return namespaceObject;
        }
        #endregion
    }
}
