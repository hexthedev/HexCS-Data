using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Serialization
{
    /// <summary>
    /// The format used to generate the serailized output
    /// </summary>
    public enum ESerializationFormat
    {
        /// <summary>
        /// This will pack the data as much as possible, eliminating useless whitespace and
        /// unneeded data like keys
        /// </summary>
        Packet = 0,

        /// <summary>
        /// This will serialize into a human readable format
        /// </summary>
        Human = 1
    }
}
