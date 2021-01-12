using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HexCS.Data.Runtime;

namespace HexCS.Data.Generation.Yaml
{
    /// <summary>
    /// Generates a YAML list
    /// </summary>
    public class GTInterField : AGenerator
    {
        /// <summary>
        /// If true, appends new line after generating object
        /// </summary>
        public bool AppendNewLine = false;

        /// <summary>
        /// If true, treats the field as a list element, 
        /// which has different generation rules
        /// </summary>
        public bool FieldIsListElement = false;

        /// <summary>
        /// if true, surpresses the indent of a field name
        /// </summary>
        public bool SupressIndent = false;

        /// <summary>
        /// The object to generate
        /// </summary>
        public InterField Field;

        /// <summary>
        /// Construct a GTObject to take some primative as an object and 
        /// </summary>
        /// <param name="sb"></param>
        public GTInterField(StringBuilder sb) : base(sb) { }

        /// <summary>
        /// Empty for internal purposes
        /// </summary>
        public GTInterField() : base() { }

        /// <summary>
        /// Set required fields
        /// </summary>
        public void SetRequired(InterField field)
        {
            Field = field;
        }

        /// <inheritdoc/>
        public override void Generate()
        {
            if (!FieldIsListElement)
            {
                if(SupressIndent) OutputBuilder.Append($"{Field.Name}:");
                else OutputBuilder.Append($"{IndentProvider.IndentString}{Field.Name}:");
            }

            if (Field.IsArray)
            {
                OutputBuilder.AppendLine();
                GTList gto2 = CreateInternalGenerator<GTList>();
                gto2.List = Field;
                gto2.Generate();
            } 
            else
            {
                switch (Field.Type)
                {
                    case EDataType.Data:
                        if(!FieldIsListElement) OutputBuilder.AppendLine();
                        GTInterField gto1 = CreateInternalGenerator<GTInterField>();
                        InterObject inter = Field.Object as InterObject;                    

                        if(inter != null)
                        {
                            if (inter.Fields.Length == 0) break;
                            
                            IndentProvider.Indent++;
                            for(int i = 0; i < inter.Fields.Length; i++)
                            {
                                gto1.Field = inter.Fields[i];
                                gto1.Generate();
                            }
                            IndentProvider.Indent--;
                        }

                        break;

                    default:
                        if(!FieldIsListElement)OutputBuilder.Append(" ");
                        GTPrimative gtp = CreateInternalGenerator<GTPrimative>();                    
                        gtp.Primative = Field.Object;
                        gtp.Generate();
                        break;
                }
            }

            if (AppendNewLine) OutputBuilder.AppendLine();
        }
    }
}
