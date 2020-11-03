using System;
using System.Collections.Generic;
using System.Text;
using HexCS.Core;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Generates a CSharp function
    /// </summary>
    public class GTInterface : AInheritableGenerator, IInterface, INamespaceObject
    {
        private List<IInterfaceDefinition> _definitions = new List<IInterfaceDefinition>();

        #region API
        /// <summary>
        /// Name of the interface
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Construct GTInterface. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public GTInterface(StringBuilder output) : base(output)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTInterface() : base() { }

        /// <summary>
        /// Set the required parameters for this generator
        /// </summary>
        public void SetRequired(string name, params EKeyword[] keywords)
        {
            Name = name;
            Add_Keywords(keywords);
        }

        /// <inheritdoc/>
        public override void Generate()
        {
            GenerateComment();
            GenerateAttributes();

            OutputBuilder.AppendLine($"{IndentProvider.IndentString}{Keywords.ToKeywordString()}interface {Name}{GenericTypes.ToGenericTypeString()}{Inheritances.ToInheritanceString()}");
            GenerateWhereConstraints(this);
            OutputBuilder.AppendLine($"{IndentProvider.IndentString}{{");

            IndentProvider.Indent++;
            OutputBuilder.AppendCharacterSeparatedCollection(
                _definitions, (s,e) => { e.Generate(); s.AppendLine(); }, "\r\n" 
            );
            IndentProvider.Indent--;

            OutputBuilder.Append($"{IndentProvider.IndentString}}}");
        }

        /// <summary>
        /// returns a generator for a interface definition
        /// </summary>
        /// <returns>Property Function Generator</returns>
        public T Generate_Definition<T>() where T: IInterfaceDefinition, new()
        {
            T def = CreateInternalGenerator<T>();
            _definitions.Add(def);
            return def;
        }
        #endregion
    }
}
