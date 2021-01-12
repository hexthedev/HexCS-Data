using System.Text;
using NUnit.Framework;
using HexCS.Data.Generation.CSharp;

namespace HexCSTests.Data
{
    [TestFixture]
    public class GTUsingsTests
    {
        [Test]
        public void Works()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTUsings usings = new GTUsings(sb))
            {
                usings.SetRequired("System", "System.Collections");
            }

            string usingsOutput = sb.ToString();

            // Assert
            Assert.That(usingsOutput == "using System;\r\nusing System.Collections;");
        }
    }
}