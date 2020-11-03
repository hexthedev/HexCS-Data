using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Runtime
{
    /// <summary>
    /// All objects that are compatible with HexCS.Data serialization
    /// techniques must inherit the IData interface, which exposes methods for transforming
    /// the runtime object into an intemediate type. 
    /// </summary>
    public interface IData
    {
        /// <summary>
        /// <para>DataLayout corresponds wtih the KeyValuePair`string, object>[]` output by the 
        /// ConvertToIntermediate function. When outputting the intermidate type, each element
        /// intemerdiate[i] should correspond to the DataLayout[i]. This facilitates conversion.
        /// TryConstructFromIntermediate will use this infomation to deserialize the intemediate.</para>
        /// 
        /// 
        /// <para>DataLayout does not follow data during conversion.</para>
        /// </summary>
        EDataType[] DataLayout { get; }

        /// <summary>
        /// Used to express the current runtime object as a KeyValuePair[],
        /// the intemediate type used in all HexCS.Data serialization
        /// </summary>
        /// <returns></returns>
        InterObject ConvertToIntermediate();

        /// <summary>
        /// Used to construct an instance of the object from intermediate data.
        /// If true, the object this is called upon will contain data as if it were
        /// constructed from the intermediate type
        /// </summary>
        /// <param name="intermediate"></param>
        /// <returns></returns>
        bool TryConstructFromIntermediate(InterObject intermediate);
    }
}
