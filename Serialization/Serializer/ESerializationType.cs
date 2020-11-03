using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Serialization
{
    /// <summary>
    /// The type of file to serialize the object to
    /// </summary>
    public enum ESerializationSyntax
    {
        /// <summary>
        /// 001010100010011
        /// </summary>
        Binary = 0,

        /// <summary>
        /// Can't write example cause the syntax is same as the comments
        /// </summary>
        XML = 1,

        /// <summary>
        /// { "name" : "value" }
        /// </summary>
        JSON = 2,

        /// <summary>
        /// name
        ///   - value1
        ///   - value2
        /// </summary>
        YAML = 3
    }
}
