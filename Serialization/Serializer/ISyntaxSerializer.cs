using System;
using System.Collections.Generic;
using System.Text;
using HexCS.Data.Runtime;

namespace HexCS.Data.Serialization
{
    /// <summary>
    /// An ISerializer is capable of serializing data into and of the supported
    /// ESerializationFormats in any ESerailizationType
    /// </summary>
    public interface ISyntaxSerializer
    {
        /// <summary>
        /// The Format to use when serializing. Autodetected when deserializing
        /// </summary>
        ESerializationFormat Format { get; set; }

        /// <summary>
        /// The Serialization type to use when serializing. Autodetected when deserializing
        /// </summary>
        ESerializationSyntax Syntax { get; }

        /// <summary>
        /// Serializes the provided intermediate into data. Uses Format and Type
        /// to determine output
        /// </summary>
        /// <param name="intermediate"></param>
        /// <returns></returns>
        byte[] Serialize(IData intermediate);

        /// <summary>
        /// Converts data to the intermediate type. This method should autodetect the Syntax and Format.
        /// Syntax is based on the serialization indicator character first byte. The Format is based on
        /// the first argument deserialized from the object which is type dependent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        IData Deserialize<T>(byte[] data) where T:IData;
    }
}
