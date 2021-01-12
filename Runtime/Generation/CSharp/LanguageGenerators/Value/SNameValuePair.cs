using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Used in value constructors. Name = Value. THe name is a string and the value
    /// is capable of generating a value
    /// </summary>
    public struct SNameValuePair
    {
        /// <summary>
        /// Name label used to name the value
        /// </summary>
        public string Name;

        /// <summary>
        /// The generator that can generate the value
        /// </summary>
        public IValue Value;
    }
}
