using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// A generator that allows the user to supply an xml comment
    /// </summary>
    public interface ICommentableGenerator : IGenerator
    {
        /// <summary>
        /// Returns a comment generator so that a comment can be added. 
        /// Multiple calls to this funciton will override previous comments
        /// </summary>
        /// <returns>Comment Generator</returns>
        T Generate_Comment<T>() where T : IComment, new();
    }
}
