using System;
using System.Xml.Linq;

namespace HexCS.Data.Serialization
{
    /// <summary>
    /// Contains useful utilities for work with Linq XML
    /// </summary>
    public static class UTXElement
    {
        /// <summary>
        /// Gets the value of an attribute or returns a 
        /// default value if the attribute cannot be found
        /// </summary>
        public static string GetAttributeOrDefault(this XElement target, XName attributeName, string defaultValue)
        {
            XAttribute attribute = target.Attribute(attributeName);

            if (attribute == null)
            {
                return defaultValue;
            }

            return attribute.Value;
        }

        /// <summary>
        /// Gets the value of an attribute or throws error
        /// </summary>
        public static string GetAttributeOrError(this XElement target, XName attributeName)
        {
            XAttribute attribute = target.Attribute(attributeName);

            if (attribute == null)
            {
                throw new Exception($"Error: XML node {target.Name} does not contain attribute {attributeName.LocalName}");
            }

            return attribute.Value;
        }

        /// <summary>
        /// Adds a child to the element and returns the new child
        /// </summary>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static XElement AddChild(this XElement target, string name)
        {
            XElement newElement = new XElement(name);
            target.Add(newElement);
            return newElement;
        }
    }
}
