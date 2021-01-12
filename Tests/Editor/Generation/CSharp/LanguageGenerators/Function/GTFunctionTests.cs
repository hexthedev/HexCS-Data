using System.Text;
using NUnit.Framework;
using HexCS.Data.Generation.CSharp;

namespace HexCSTests.Data
{
    [TestFixture]
    public class GTFunctionTests
    {
        [Test]
        public void Works()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTFunction_Implementation func = new GTFunction_Implementation(sb))
            {
                func.SetRequired("void", "TestFunction", EKeyword.PUBLIC, EKeyword.STATIC);

                func.Add_Parameters(
                    new Parameter_Basic("int", "x"),
                    new Parameter_Basic("string", "y", "\"hello\"", EKeyword.REF)
                ); ;

                func.Add_Statements("int w = x;", "string z = y;");
            }

            string output = sb.ToString();
            string expectedOutput = "public static void TestFunction(int x, ref string y = \"hello\")\r\n{\r\n   int w = x;\r\n   string z = y;\r\n}";

            // Assert
            Assert.That(output == expectedOutput);
        }
    }
}