using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Represents a parameter
    /// </summary>
    public class Parameter_Basic : IParameterBlock
    {
        /// <summary>
        /// Parameter keywords
        /// </summary>
        public KeywordsCollection Keywords { get; private set; } = new KeywordsCollection();

        /// <summary>
        /// Parameter type
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Parameter Names
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The default value of the parameter
        /// </summary>
        public string DefaultValue { get; set; } = string.Empty;

        /// <summary>
        /// Default constructor for custom constructing
        /// </summary>
        public Parameter_Basic() { }

        /// <summary>
        /// Make a basic paramter
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="name">name</param>
        public Parameter_Basic(string type, string name)
        {
            Type = type;
            Name = name;
        }

        /// <summary>
        /// Make a basic paramter
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="name">name</param>
        /// <param name="defaultValue">default value</param>
        /// <param name="keywords">keywords preceeeding the parameter</param>
        public Parameter_Basic(string type, string name, string defaultValue, params EKeyword[] keywords) : this(type, name)
        {
            DefaultValue = defaultValue;
            Keywords.AddKeywords(keywords);
        }

        /// <summary>
        /// Get this as a standard paramter string
        /// </summary>
        /// <returns></returns>
        public string ToParameterString()
        {
            string defaultVal = string.IsNullOrEmpty(DefaultValue) ? "" : $" = {DefaultValue}";
            return $"{Keywords.ToKeywordString()}{Type} {Name}{defaultVal}";
        }
    }
}
