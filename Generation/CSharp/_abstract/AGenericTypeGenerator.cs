using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// A generator that can have comments, attributes and keywords
    /// </summary>
    public abstract class AGenericTypeGenerator : ACommonCSharpGenerator, IGenericTypeGenerator
    {
        #region Internal API 
        /// <summary>
        /// Empty generator. Note that empty generators will not function correctly. The
        /// empty constructor is used internally.
        /// </summary>
        internal AGenericTypeGenerator() : base() { }
        #endregion

        #region Protected API
        /// <summary>
        /// Collection containing generic types
        /// </summary>
        protected GenericTypeCollection GenericTypes { get; private set; } = new GenericTypeCollection();

        /// <summary>
        /// Generates where contrains in the format a class would normally take. 
        /// This means that the indent is increased by one and each where contraint is
        /// printed on it's own line
        /// 
        ///    where T : IList
        ///    where T2 : ICollection
        /// </summary>
        /// <param name="generator"></param>
        protected static void GenerateWhereConstraints(AGenericTypeGenerator generator)
        {
            generator.IndentProvider.Indent++;
            foreach (string constraint in generator.GenericTypes.ToTypeConstrainStrings())
            {
                generator.OutputBuilder.AppendLine($"{generator.IndentProvider.IndentString}{constraint}");
            }
            generator.IndentProvider.Indent--;
        }
        #endregion

        #region Public API
        /// <summary>
        /// Construct AAttributeableGenerator. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public AGenericTypeGenerator(StringBuilder output) : base(output)
        {
        }

        /// <inheritdoc />
        public void Add_GenericTypes(params string[] types) => GenericTypes.Types.AddRange(types);

        /// <inheritdoc />
        public void Add_GenericTypeConstraints(params string[] constraints) => GenericTypes.Constraints.AddRange(constraints);
        #endregion
    }
}
