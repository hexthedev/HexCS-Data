using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Outs a string for CSharp function argument
    /// </summary>
    public interface IArg
    {
        /// <summary>
        /// Returns a string representing the argument
        /// </summary>
        /// <returns></returns>
        string ToArgumentString();
    }
}
