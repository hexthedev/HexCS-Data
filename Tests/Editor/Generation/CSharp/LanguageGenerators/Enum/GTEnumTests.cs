using System.Text;
using NUnit.Framework;
using HexCS.Data.Generation.CSharp;

namespace HexCSTests.Data
{
    [TestFixture]
    public class GTEnumTests
    {
        [Test]
        public void Works()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTEnum e = new GTEnum(sb))
            {
                e.SetRequired(
                    "Test", 
                    new EnumValue[] { new EnumValue("test1"), new EnumValue("test2", "1") },
                    EKeyword.PUBLIC
                );

                using (GTComment com = e.Generate_Comment<GTComment>())
                {
                    com.SetRequired("This is an enum");
                }

                using (GTAttribute att = e.Generate_Attribute<GTAttribute>())
                {
                    att.SetRequired("Attrib");
                }
            }

            string output = sb.ToString();
            string expected = "/// <summary>\r\n/// This is an enum\r\n/// </summary>\r\n[Attrib]\r\npublic enum Test {\r\n   test1,\r\n   test2 = 1\r\n}";

            // Assert
            Assert.That(output == expected);
        }
    }
}