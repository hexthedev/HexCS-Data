using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// A generator that allows comments and attributes to be applied
    /// </summary>
    public interface IInheritableGenerator : IGenericTypeGenerator
    {
        /// <summary>
        /// Add inheritance types to the generator
        /// </summary>
        /// <param name="inheritanceTypes"></param>
        void Add_Inheritances(params string[] inheritanceTypes);
    }
}
