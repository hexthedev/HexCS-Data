using System;
using System.Collections.Generic;
using System.Text;
using HexCS.Core;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// A Collection of inherited types
    /// </summary>
    public class InheritanceTypeCollection
    {
        #region API
        /// <summary>
        /// The paramaters in the collection
        /// </summary>
        public List<string> InheritedTypes { get; private set; } = new List<string>();

        /// <summary>
        /// Returns an inheritance string " : Type, Type, Type"
        /// </summary>
        /// <returns></returns>
        public string ToInheritanceString()
        {
            if (InheritedTypes.Count == 0) return string.Empty;

            StringBuilder sb = new StringBuilder();

            sb.Append(" : ");

            sb.AppendCharacterSeparatedCollection(
                InheritedTypes, (s, e) => sb.Append(e), ", "
            );

            return sb.ToString();
        }
        #endregion
    }
}
