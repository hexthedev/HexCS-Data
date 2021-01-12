using NUnit.Framework;
using HexCS.Data.Generation.CSharp;

namespace HexCSTests.Data
{
    [TestFixture]
    public class ParameterCollectionTests
    {
        [Test]
        public void Works()
        {
            // Arrange
            ParameterCollection col = new ParameterCollection();

            col.AddParameters(
                new Parameter_Basic("int", "test"),
                new Parameter_Basic("string", "test2", "\"hello\"", EKeyword.OUT),
                new Parameter_Basic("int", "test3", "3")
            );

            // Act
            string test = col.ToFunctionInputString();

            // Assert
            Assert.That(test == "int test, out string test2 = \"hello\", int test3 = 3");
        }
    }
}