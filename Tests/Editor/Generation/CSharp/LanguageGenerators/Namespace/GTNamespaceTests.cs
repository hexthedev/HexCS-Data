using System.Text;
using NUnit.Framework;
using HexCS.Data.Generation.CSharp;

namespace HexCSTests.Data
{
    [TestFixture]
    public class GTNamespaceTests
    {
        [Test]
        public void Works()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTNamespace nsp = new GTNamespace(sb))
            {
                nsp.NameSpace = "System.Test";

                using (GTClass cls = nsp.Generate_NamespaceObject<GTClass>())
                {
                    cls.SetRequired("TestClass", EKeyword.PUBLIC, EKeyword.STATIC);
                }
            }

            string output = sb.ToString();
            string expected = "namespace System.Test\r\n{\r\n   public static class TestClass\r\n   {\r\n   }\r\n}";

            // Assert
            Assert.That(output == expected);
        }
    }
}