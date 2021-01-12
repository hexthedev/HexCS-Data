using NUnit.Framework;
using HexCS.Data.Generation.CSharp;

namespace HexCSTests.Data
{
    [TestFixture]
    public class ParameterTests
    {
        [Test]
        public void Works()
        {
            // Arrange
            Parameter_Basic test1 = new Parameter_Basic("int", "test");

            Parameter_Basic test2 = new Parameter_Basic("int", "test");
            test2.DefaultValue = "3";
            test2.Keywords.AddKeywords(EKeyword.REF);

            // Act
            string test1out = test1.ToParameterString();
            string test2out = test2.ToParameterString();

            // Assert
            Assert.That(test1.ToParameterString() == "int test");
            Assert.That(test2.ToParameterString() == "ref int test = 3");
        }
    }
}