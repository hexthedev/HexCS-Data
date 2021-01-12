using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Generator for when a value is amn object initializer. Example: new SObjectName { This = That, AnotherThing = 7 }
    /// </summary>
    public class GTValue_ObjectInitializer : AGenerator, IValue
    {
        private List<SNameValuePair> _values = new List<SNameValuePair>();

        /// <summary>
        /// The name of the object. 
        /// </summary>
        public string ObjectType;

        /// <summary>
        /// Should this start generating at the current generation point, or should a new line be used.
        /// </summary>
        public bool IsInline = true;

        /// <summary>
        /// Constructor
        /// </summary>
        public GTValue_ObjectInitializer(StringBuilder builder) : base(builder) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public GTValue_ObjectInitializer() : base() { }

        /// <summary>
        /// Set required parameters for this generator
        /// </summary>
        /// <param name="objectName">The type of the object</param>
        /// <param name="isInline">Shoudl generation start on the line or down a line</param>
        public void SetRequired(string objectName, bool isInline = true)
        {
            ObjectType = objectName;
            IsInline = isInline;
        }

        /// <inheritdoc />
        public override void Generate()
        {
            if (!IsInline)
            {
                OutputBuilder.AppendLine();
                IndentProvider.Indent++;
                OutputBuilder.Append(IndentProvider.IndentString);
            }

            OutputBuilder.AppendLine($"new {ObjectType}() {{");
            IndentProvider.Indent++;

            for (int i = 0; i<_values.Count; i++)
            {
                OutputBuilder.Append($"{IndentProvider.IndentString}{_values[i].Name} = ");
                _values[i].Value.Generate();
                
                if(i != _values.Count - 1)
                {
                    OutputBuilder.AppendLine(",");
                } else
                {
                    OutputBuilder.AppendLine();
                }
            }

            IndentProvider.Indent--;
            OutputBuilder.Append($"{IndentProvider.IndentString}}}");
            if (!IsInline) IndentProvider.Indent--;
        }

        /// <summary>
        /// Generate a named value to be initlaized in the object initalizer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T Generate_NamedValue<T>(string name) where T : IValue, new()
        {
            T valGenerator = CreateInternalGenerator<T>();
            _values.Add(new SNameValuePair() { Name = name, Value = valGenerator });
            return valGenerator;
        }
    }
}
