using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Outs an enum value string
    /// </summary>
    public interface IEnumValue
    {
        /// <summary>
        /// Returns a string representing enum value
        /// </summary>
        /// <returns></returns>
        string ToEnumValueString();
    }
}
