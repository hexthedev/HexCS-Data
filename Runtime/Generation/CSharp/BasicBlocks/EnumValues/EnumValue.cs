using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Represents an enum value. 
    /// </summary>
    public class EnumValue : IEnumValue
    {
        /// <summary>
        /// Enum value name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Enum value
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Enum value
        /// </summary>
        /// <param name="name">Name of enum arg</param>
        /// <param name="value">Value of enum arg</param>
        public EnumValue(string name, string value = null)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Get this as a standard paramter string
        /// </summary>
        /// <returns></returns>
        public string ToEnumValueString()
        {
            if (!string.IsNullOrEmpty(Value)) return $"{Name} = {Value}";
            return Name;
        }
    }
}
