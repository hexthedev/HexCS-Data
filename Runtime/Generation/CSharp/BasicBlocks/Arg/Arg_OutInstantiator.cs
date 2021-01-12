using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Represents an argument for a function. 
    /// This arg instanciates an out object with the syntax out type x
    /// </summary>
    public class Arg_OutInstantiator : IArg
    {
        /// <summary>
        /// Name of paramter to apply arg to
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Type of instantiation
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Arg with single value
        /// </summary>
        /// <param name="name">Name of paramter to apply arg</param>
        /// <param name="type">type of instantiation</param>
        public Arg_OutInstantiator(string name, string type)
        {
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Get this as a standard paramter string
        /// </summary>
        /// <returns></returns>
        public string ToArgumentString()
        {
            return $"out {Type} {Name}";
        }
    }
}
