using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// A generator that allows the user to supply an xml comment
    /// </summary>
    public abstract class ACommentableGenerator : AGenerator, ICommentableGenerator
    {
        private IComment _comment;

        #region Internal API 
        /// <summary>
        /// Empty generator. Note that empty generators will not function correctly. The
        /// empty constructor is used internally.
        /// </summary>
        internal ACommentableGenerator() : base() { } 
        #endregion

        #region Protected API
        /// <summary>
        /// If a comment has been supplied, generates the comment
        /// </summary>
        protected void GenerateComment() => _comment?.Generate();
        #endregion

        #region Public API
        /// <summary>
        /// Construct ACommentableGenerator. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public ACommentableGenerator(StringBuilder output) : base(output)
        {
        }

        /// <inheritdoc />
        public T Generate_Comment<T>() where T : IComment, new()
        {
            T generator = CreateInternalGenerator<T>();
            _comment =  generator;
            return generator;
        }
        #endregion
    }
}
