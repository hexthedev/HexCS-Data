using System.Text;
using NUnit.Framework;
using HexCS.Data.Generation.CSharp;

namespace HexCSTests.Data
{
    [TestFixture]
    public class GTStructTests
    {
        [Test]
        public void Works()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTStruct cls = new GTStruct(sb))
            {
                cls.SetRequired("Test", EKeyword.PUBLIC, EKeyword.STATIC);

                cls.Add_Inheritances( "TestInherit" );

                cls.Add_GenericTypes("T", "T2");
                cls.Add_GenericTypeConstraints("T2 : IList<T>");

                using (GTField f = cls.Generate_Field<GTField>())
                {
                    f.SetRequired("int", "Field1", EKeyword.PRIVATE);
                }

                using (GTProperty_OneLine prop = cls.Generate_Property<GTProperty_OneLine>())
                {
                    prop.SetRequired("string", "Prop1", EKeyword.PUBLIC);
                    prop.GetFunction = new GTProperty_OneLine.FunctionParams { IsPresent = true };
                }

                using (GTProperty_MultiLine prop = cls.Generate_Property<GTProperty_MultiLine>())
                {
                    prop.SetRequired("string", "Prop2", EKeyword.PRIVATE);

                    prop.Generate_DefaultValue<GTValue>().SetRequired("\"1\"");

                    prop.GetFunction = new GTProperty_MultiLine.FunctionParams {
                        IsPresent = true,
                        Statements = new string[] { "string y = \"This is a thing\";", "return y;" },
                        Keywords = new KeywordsCollection(EKeyword.PROTECTED)
                    };
                }

                using (GTFunction_Implementation func = cls.Generate_Function<GTFunction_Implementation>())
                {
                    func.SetRequired("void", "Func1", EKeyword.PUBLIC, EKeyword.STATIC);
                    func.Add_Parameters(new Parameter_Basic("int", "x"));
                    func.Add_Statements("Field1 = x;");
                }
            }

            string output = sb.ToString();
            string expected = @"public static struct Test<T, T2> : TestInherit
   where T2 : IList<T>
{
   private int Field1;

   public string Prop1 { get; }

   private string Prop2 {
      protected get {
         string y = ""This is a thing"";
         return y;
      }
   } = ""1"";

   public static void Func1(int x)
   {
      Field1 = x;
   }

}";
        
            // Assert
            Assert.That(output == expected);
        }
    }
}