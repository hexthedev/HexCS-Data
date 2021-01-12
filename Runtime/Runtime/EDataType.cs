using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Runtime
{
    /// <summary>
    /// Data types that are supported by the HexCS data
    /// intemediate type
    /// </summary>
    public enum EDataType : byte
    {
        /// <summary>
        /// Boolean type
        /// </summary>
        Bool = 0,

        /// <summary>
        /// Integer Type
        /// </summary>
        Int = 1,

        /// <summary>
        /// Float Type
        /// </summary>
        Float = 2,

        /// <summary>
        /// String type
        /// </summary>
        String = 3,

        /// <summary>
        /// Nested IData type
        /// </summary>
        Data = 254,

        /// <summary>
        /// The type must be inferred somehow. Normally during IData
        /// TryConstructFromIntermediate
        /// </summary>
        Auto = 255
    }

    /// <summary>
    /// Utility methods used with ESupportData
    /// </summary>
    public static class UTESupportedData
    {

        /// <summary>
        /// Try and parse object as an int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool TryParseInt(object obj, out int val)
        {
            if(!(obj is int))
            {
                val = default;
                return false;
            }

            val = (int)obj;
            return true;

        }
    }
}
 