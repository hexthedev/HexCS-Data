using System.Text;
using NUnit.Framework;
using HexCS.Data.Generation.CSharp;

namespace HexCSTests.Data
{
    [TestFixture]
    public class GTInterfaceDefinitionTests
    {
        [Test]
        public void Works()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTInterfaceDefinition_Function test = new GTInterfaceDefinition_Function(sb))
            {
                test.SetRequired("void", "Test1");

                test.Add_Paramaters(
                    new Parameter_Basic("int", "test")
                );
            }

            string output1 = sb.ToString();
            string expected1 = "void Test1(int test);";
            sb.Clear();

            using (GTInterfaceDefinition_Property test = new GTInterfaceDefinition_Property(sb))
            {
                test.SetRequired("int", "Test1", true, false);
            }

            string output2 = sb.ToString();
            string expected2 = "int Test1 { get; }";
            sb.Clear();

            // Assert
            Assert.That(output1 == expected1);
            Assert.That(output2 == expected2);
        }
    }
}