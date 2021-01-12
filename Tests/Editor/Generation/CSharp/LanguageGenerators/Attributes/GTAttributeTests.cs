using System.Text;
using NUnit.Framework;
using HexCS.Data.Generation.CSharp;

namespace HexCSTests.Data
{
    [TestFixture]
    public class GTAttributeTests
    {
        [Test]
        public void Works()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTAttribute attr = new GTAttribute(sb))
            {
                attr.SetRequired("Test");

                attr.Add_Args(
                    new Arg_Basic("3"),
                    new Arg_Named("test", "5.6"),
                    new Arg_OutInstantiator("test2", "Godzilla")
                );
            }

            string output = sb.ToString();
            string expected = "[Test(3, test = 5.6, out Godzilla test2)]";

            sb.Clear();
            using (GTAttribute attr = new GTAttribute(sb))
            {
                attr.Name = "Test";
            }

            string output2 = sb.ToString();
            string expected2 = "[Test]";

            // Assert
            Assert.That(output == expected);
            Assert.That(output2 == expected2);
        }
    }
}