using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Generator for when a value is amn object initializer. Example: new SObjectName { This = That, AnotherThing = 7 }
    /// </summary>
    public class GTValue_ArrayInitializer : AGenerator, IValue
    {
        private List<IValue> _values = new List<IValue>();

        /// <summary>
        /// The type of the array. if int[] just write int
        /// </summary>
        public string ArrayType;

        /// <summary>
        /// Should this start generating at the current generation point, or should a new line be used.
        /// </summary>
        public bool IsInline = true;

        /// <summary>
        /// Constructor
        /// </summary>
        public GTValue_ArrayInitializer(StringBuilder builder) : base(builder) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public GTValue_ArrayInitializer() : base() { }

        /// <summary>
        /// Set required parameters for this generator
        /// </summary>
        /// <param name="arrayType">The type of array. if int[] then arrayType=int</param>
        /// <param name="isInline">Should generation start on the line or down a line</param>
        public void SetRequired(string arrayType, bool isInline = true)
        {
            ArrayType = arrayType;
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

            OutputBuilder.AppendLine($"new {ArrayType}[] {{");
            IndentProvider.Indent++;

            for (int i = 0; i < _values.Count; i++)
            {
                OutputBuilder.Append(IndentProvider.IndentString);
                _values[i].Generate();

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
        public T Generate_Value<T>() where T : IValue, new()
        {
            T valGenerator = CreateInternalGenerator<T>();
            _values.Add(valGenerator);
            return valGenerator;
        }
    }
}
