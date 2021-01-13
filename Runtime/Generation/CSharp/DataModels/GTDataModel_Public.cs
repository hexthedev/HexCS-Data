using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HexCS.Data.Persistence;
using HexCS.Core;
using HexCS.Core;

namespace HexCS.Data.Generation.CSharp
{
    /// <summary>
    /// Generates a simple data model in C# where all fields are public
    /// </summary>
    public class GTDataModel_Public : AFileGenerator, INamespace
    {
        private List<IField> _fields = new List<IField>();

        #region Public API
        /// <summary>
        /// The usings that will appear at the top of the file
        /// </summary>
        public IUsings Usings { get; private set; }

        /// <summary>
        /// The namespace of the model
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// The args used to generate the model
        /// </summary>
        public Model ModelArgs { get; set; }

        /// <summary>
        /// Construct GTDataModel. Generators require an output StringBuilder (that will be generated to).
        /// </summary>
        /// <param name="output">StringBuilder Generate() will output to</param>
        /// <param name="path">path to the file to write to</param>
        /// <param name="encoding">encoding to write file in</param>
        public GTDataModel_Public(StringBuilder output, PathString path = null, Encoding encoding = null) : base(output, path, encoding)
        {
        }

        /// <summary>
        /// Internal empty constructor
        /// </summary>
        public GTDataModel_Public() : base() { }

        /// <summary>
        /// Set the required parameters for this generator
        /// </summary>
        public void SetRequired(Model modelArgs)
        {
            ModelArgs = modelArgs;
        }

        /// <inheritdoc/>
        public override void Generate()
        {
            using (GTFile file = new GTFile(OutputBuilder, Path, Encoding))
            {
                file.SupressFileGeneration = SupressFileGeneration; 

                Usings?.Generate();

                using( GTNamespace nsp = file.Generate_Namespace<GTNamespace>())
                {
                    nsp.NameSpace = Namespace;

                    using (GTClass cls = nsp.Generate_NamespaceObject<GTClass>())
                    {
                        cls.Add_Keywords(EKeyword.PUBLIC);

                        if (!string.IsNullOrEmpty(ModelArgs.Comment))
                        {
                            using (GTComment com = cls.Generate_Comment<GTComment>())
                            {
                                com.Summary = ModelArgs.Comment;
                            }
                        }

                        cls.Name = ModelArgs.ModelName;

                        foreach(ModelField field in ModelArgs.Fields)
                        {
                            using (GTField f = cls.Generate_Field<GTField>())
                            {
                                if (!string.IsNullOrEmpty(field.Comment))
                                {
                                    using (GTComment com = f.Generate_Comment<GTComment>())
                                    {
                                        com.Summary = field.Comment;
                                    }
                                }

                                f.Add_Keywords(EKeyword.PUBLIC);
                                f.Name = field.Name;
                                f.Type = field.Type;

                                if (!string.IsNullOrEmpty(field.DefaultValue))
                                {
                                    f.Generate_DefaultValue<GTValue>().SetRequired(field.DefaultValue);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// returns a generator for a field
        /// </summary>
        /// <returns>Property Function Generator</returns>
        public T Generate_Field<T>() where T : IField, new()
        {
            T field = CreateInternalGenerator<T>();
            _fields.Add(field);
            return field;
        }

        /// <summary>
        /// returns a generator for file usings. Extra calls will override last value
        /// </summary>
        /// <returns>Property Function Generator</returns>
        public T Generate_Usings<T>() where T : IUsings, new()
        {
            T namespaceObject = CreateInternalGenerator<T>();
            Usings = namespaceObject;
            return namespaceObject;
        }
        #endregion

        /// <summary>
        /// Arguments for generting a model
        /// </summary>
        public struct Model
        {
            /// <summary>
            /// Name of the model
            /// </summary>
            public string ModelName;

            /// <summary>
            /// Comment applied tot he model
            /// </summary>
            public string Comment;

            /// <summary>
            /// The fields the model contains
            /// </summary>
            public ModelField[] Fields;
        }

        /// <summary>
        /// Arguments required to generate a model field
        /// </summary>
        public struct ModelField
        {
            /// <summary>
            /// Name of the model
            /// </summary>
            public string Name;

            /// <summary>
            /// Field type
            /// </summary>
            public string Type;

            /// <summary>
            /// Default value of the model
            /// </summary>
            public string DefaultValue;

            /// <summary>
            /// Comment applied tot he model
            /// </summary>
            public string Comment;
        }
    }
}
