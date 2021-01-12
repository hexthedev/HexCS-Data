using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Generator for when a value is amn object initializer. Example: new SObjectName { This = That, AnotherThing = 7 }
    /// </summary>
    public class GTValue : AGenerator, IValue
    {
        private List<SNameValuePair> _values = new List<SNameValuePair>();

        /// <summary>
        /// The name of the object. 
        /// </summary>
        public string Value;

        /// <summary>
        /// Constructor
        /// </summary>
        public GTValue(StringBuilder builder) : base(builder) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public GTValue() : base() { }

        /// <summary>
        /// Set required parameters for this generator
        /// </summary>
        /// <param name="val">The value</param>
        public void SetRequired(string val)
        {
            Value = val;
        }

        /// <inheritdoc />
        public override void Generate() => OutputBuilder.Append(Value);
    }
}
