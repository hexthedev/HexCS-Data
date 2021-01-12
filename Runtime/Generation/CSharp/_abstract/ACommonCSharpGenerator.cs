using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// A generator that can have comments, attributes and keywords
    /// </summary>
    public abstract class ACommonCSharpGenerator : ACommentableGenerator, ICommonCSharpGenerator
    {
        private List<IAttribute> _attributes = new List<IAttribute>();

        #region Internal API 
        /// <summary>
        /// Empty generator. Note that empty generators will not function correctly. The
        /// empty constructor is used internally.
        /// </summary>
        public ACommonCSharpGenerator() : base() { }
        #endregion

        #region Protected API
        /// <summary>
        /// Generates attributes supplied
        /// </summary>
        protected void GenerateAttributes()
        {
            foreach(IAttribute attrib in _attributes)
            {
                attrib.Generate();
                OutputBuilder.AppendLine();
            }
        }

        /// <summary>
        /// Keywords proceeding enum
        /// </summary>
        protected KeywordsCollection Keywords { get; private set; } = new KeywordsCollection();
        #endregion

        #region Public API
        /// <summary>
        /// Construct AAttributeableGenerator. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public ACommonCSharpGenerator(StringBuilder output) : base(output)
        {
        }

        /// <inheritdoc />
        public T Generate_Attribute<T>() where T : IAttribute, new()
        {
            T generator = CreateInternalGenerator<T>();
            _attributes.Add(generator);
            return generator;
        }

        /// <inheritdoc />
        public void Add_Keywords(params EKeyword[] keywords) => Keywords.AddKeywords(keywords);
        #endregion
    }
}
