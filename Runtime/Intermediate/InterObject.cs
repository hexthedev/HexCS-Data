using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Runtime
{
    /// <summary>
    /// Intermediate type representation of some IData object. Objects contain 
    /// fields, which are essentially named values
    /// </summary>
    public class InterObject
    {
        /// <summary>
        /// Required for InterObjects which represent IData. Needed 
        /// to have an object name for list elements
        /// </summary>
        public string Name;

        /// <summary>
        /// Fields contained within the object. These are named fields,
        /// put order alos matters. The field names are present for generation
        /// of serializaiton files. Order is required for generic deserialization
        /// </summary>
        public InterField[] Fields;
        
        /// <summary>
        /// Construct InterObject
        /// </summary>
        /// <param name="fields"></param>
        public InterObject(params InterField[] fields)
        {
            Fields = fields;
        }

        /// <summary>
        /// Construct InterObject
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="typeName"></param>
        public InterObject(string typeName, params InterField[] fields)
        {
            Name = typeName;
            Fields = fields;
        }

        /// <summary>
        /// Checks equality based on object only, not names
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is InterObject)) return false;
            InterObject other = (InterObject)obj;

            if (Fields.Length != other.Fields.Length) return false;

            for(int i = 0; i < Fields.Length; i++)
            {
                if (!Fields[i].Equals(other.Fields[i])) return false;
            }

            return true;
        }
    }
}
