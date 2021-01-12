using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Serialization
{
    /// <summary>
    /// An IGenericSerializer is capable of serializing data into and of the supported
    /// ESerializationFormats in any ESerailizationType
    /// </summary>
    public interface IGenericSerializer : ISyntaxSerializer
    {
        /// <summary>
        /// Set the format that should be used by the serializer
        /// </summary>
        /// <param name="format"></param>
        void SetSyntax(ESerializationSyntax syntax);
    }
}
