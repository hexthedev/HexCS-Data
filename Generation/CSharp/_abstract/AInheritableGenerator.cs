using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// A generator that can have comments, attributes and keywords
    /// </summary>
    public abstract class AInheritableGenerator : AGenericTypeGenerator, IInheritableGenerator
    {
        #region Internal API 
        /// <summary>
        /// Empty generator. Note that empty generators will not function correctly. The
        /// empty constructor is used internally.
        /// </summary>
        internal AInheritableGenerator() : base() { }
        #endregion

        #region Protected API
        /// <summary>
        /// Collection containing Inheritance types supplied to the generator
        /// </summary>
        protected InheritanceTypeCollection Inheritances { get; set; } = new InheritanceTypeCollection();
        #endregion

        #region Public API
        /// <summary>
        /// Construct AAttributeableGenerator. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public AInheritableGenerator(StringBuilder output) : base(output)
        {
        }

        /// <inheritcdoc />
        public void Add_Inheritances(params string[] inheritanceTypes) => Inheritances.InheritedTypes.AddRange(inheritanceTypes);
        #endregion
    }
}
