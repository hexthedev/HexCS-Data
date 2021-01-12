using System.Text;
using NUnit.Framework;
using HexCS.Data.Generation.CSharp;

namespace HexCSTests.Data
{
    [TestFixture]
    public class GTInterfaceTests
    {
        [Test]
        public void Works()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTInterface test = new GTInterface(sb))
            {
                test.SetRequired("Test1", EKeyword.PUBLIC);

                test.Add_Inheritances("ITester");

                using ( GTInterfaceDefinition_Function def = test.Generate_Definition<GTInterfaceDefinition_Function>())
                {
                    def.SetRequired("int", "Test");

                    def.Add_Paramaters(
                        new Parameter_Basic("int", "par1"),
                        new Parameter_Basic("string", "par2")
                    );
                }

                test.Generate_Definition<GTInterfaceDefinition_Property>().SetRequired("int", "Test", false, true);
            }

            string output = sb.ToString();
            string expected= "public interface Test1 : ITester\r\n{\r\n   int Test(int par1, string par2);\r\n\r\n   int Test { set; }\r\n}";

            // Assert
            Assert.That(output == expected);
        }
    }
}