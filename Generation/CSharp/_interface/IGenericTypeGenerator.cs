using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// A generator that allows comments and attributes to be applied
    /// </summary>
    public interface IGenericTypeGenerator : ICommonCSharpGenerator
    {
        /// <summary>
        /// Add a generic type to this generator
        /// </summary>
        /// <param name="types">type to add</param>
        void Add_GenericTypes(params string[] types);

        /// <summary>
        /// Add a generic type 'where'constraint
        /// </summary>
        /// <param name="constraints">constraint to add, without the where keyword</param>
        void Add_GenericTypeConstraints(params string[] constraints);
    }
}
