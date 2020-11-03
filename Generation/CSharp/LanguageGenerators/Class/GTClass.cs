using System;
using System.Collections.Generic;
using System.Text;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Generates a CSharp function
    /// </summary>
    public class GTClass : AInheritableGenerator, IClass, INamespaceObject
    {
        private List<IField> _fields = new List<IField>();
        private List<IProperty> _properties  = new List<IProperty>();
        private List<IFunction> _functions = new List<IFunction>();

        #region API
        /// <summary>
        /// Name of the class
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Construct GTClass. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        public GTClass(StringBuilder output) : base(output)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTClass() : base() { }

        /// <summary>
        /// Set the required parameters for this generator
        /// </summary>
        public void SetRequired(string name, params EKeyword[] keywords)
        {
            Name = name;
            Add_Keywords(keywords);
        }

        /// <inheritdoc/>
        public override void Generate()
        {
            GenerateComment();
            GenerateAttributes();

            OutputBuilder.AppendLine($"{IndentProvider.IndentString}{Keywords.ToKeywordString()}class {Name}{GenericTypes.ToGenericTypeString()}{Inheritances.ToInheritanceString()}");
            GenerateWhereConstraints(this);
            OutputBuilder.AppendLine($"{IndentProvider.IndentString}{{");

            IndentProvider.Indent++;

            foreach (IField f in _fields)
            {
                f.Generate();
                OutputBuilder.AppendLine();
                OutputBuilder.AppendLine();
            }

            foreach (IProperty p in _properties)
            {
                p.Generate();
                OutputBuilder.AppendLine();
                OutputBuilder.AppendLine();
            }

            foreach (IFunction f in _functions)
            {
                f.Generate();
                OutputBuilder.AppendLine();
                OutputBuilder.AppendLine();
            }

            IndentProvider.Indent--;
            OutputBuilder.Append($"{IndentProvider.IndentString}}}");
        }

        /// <summary>
        /// returns a generator for a field
        /// </summary>
        /// <returns>Property Function Generator</returns>
        public T Generate_Field<T>() where T: IField, new()
        {
            T field = CreateInternalGenerator<T>();
            _fields.Add(field);
            return field;
        }

        /// <summary>
        /// returns a generator for a property
        /// </summary>
        /// <returns>Property Function Generator</returns>
        public T Generate_Property<T>() where T : IProperty, new()
        {
            T property = CreateInternalGenerator<T>();
            _properties.Add(property);
            return property;
        }

        /// <summary>
        /// returns a generator for a function
        /// </summary>
        /// <returns>Property Function Generator</returns>
        public T Generate_Function<T>() where T: IFunction, new()
        {
            T function = CreateInternalGenerator<T>();
            _functions.Add(function);
            return function;
        }
        #endregion
    }
}
