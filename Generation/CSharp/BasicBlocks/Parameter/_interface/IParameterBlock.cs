using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Represents a paramter block. There are multiple types of paramter blocks
    /// </summary>
    public interface IParameterBlock
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string ToParameterString();
    }
}
