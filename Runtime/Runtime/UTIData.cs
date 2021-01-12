using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Runtime
{
    /// <summary>
    /// Utility functions related to IDataObjects
    /// </summary>
    public static class UTIData
    {
        /// <summary>
        /// Returns true if the fields object is valid to construct to target IData
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static bool ValidateDataLayout(this IData data, InterField[] fields)
        {
            if (fields == null || data.DataLayout.Length != fields.Length) return false;

            for(int i = 0; i<data.DataLayout.Length; i++)
            {
                if (fields[i].Type == EDataType.Auto || data.DataLayout[i] == fields[i].Type) continue;
                return false;
            }

            return true;
        }
    }
}
