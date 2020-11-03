using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// A generator that allows comments and attributes to be applied
    /// </summary>
    public interface ICommonCSharpGenerator : ICommentableGenerator
    {
        /// <summary>
        /// Returns an attribute generator so that attributes can be applied
        /// </summary>
        /// <returns>Attribute Generator</returns>
        T Generate_Attribute<T>() where T : IAttribute, new();

        /// <summary>
        /// Add keywords to the generator
        /// </summary>
        /// <param name="keywords">keywords to add</param>
        void Add_Keywords(params EKeyword[] keywords);
    }
}
