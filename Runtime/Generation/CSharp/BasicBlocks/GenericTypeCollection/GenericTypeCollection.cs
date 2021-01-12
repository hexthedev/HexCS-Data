using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HexCS.Core;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Represents an enum value. 
    /// </summary>
    public class GenericTypeCollection
    {
        /// <summary>
        /// The types names, ex: T, T1
        /// </summary>
        public List<string> Types { get; } = new List<string>();

        /// <summary>
        /// constraints, ex: T1 : IList
        /// </summary>
        public List<string> Constraints { get; } = new List<string>();

        /// <summary>
        /// GenericTypes
        /// </summary>
        /// <param name="types">types in definition</param>
        public GenericTypeCollection(params string[] types)
        {
            Types.AddRange(types);
        }

        /// <inheritdoc />
        public string ToGenericTypeString()
        {
            if (Types.Count == 0) return string.Empty;

            StringBuilder sb = new StringBuilder();

            sb.Append("<");

            sb.AppendCharacterSeparatedCollection(
                Types, (s,e) => sb.Append(e), ", "
            );

            sb.Append(">");

            return sb.ToString();
        }

        /// <inheritdoc />
        public string[] ToTypeConstrainStrings() => Constraints.Select(s => $"where {s}").ToArray();
    }
}
