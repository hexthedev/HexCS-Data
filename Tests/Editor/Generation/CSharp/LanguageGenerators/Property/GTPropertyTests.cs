using System.Text;
using NUnit.Framework;
using HexCS.Data.Generation.CSharp;

namespace HexCSTests.Data
{
    [TestFixture]
    public class GTPropertyTests
    {
        [Test]
        public void Works_OneLiner()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTProperty_OneLine prop = new GTProperty_OneLine(sb))
            {
                prop.SetRequired("int", "Prop", EKeyword.PUBLIC);

                prop.Generate_DefaultValue<GTValue>().SetRequired("5");

                prop.GetFunction = new GTProperty_OneLine.FunctionParams { IsPresent = true };

                prop.SetFunction = new GTProperty_OneLine.FunctionParams {
                    IsPresent = true,
                    Statement = "_x = value;",
                    Keywords = new KeywordsCollection(EKeyword.PRIVATE)
                };
            }

            string output = sb.ToString();
            string expected = "public int Prop { get; private set => _x = value; } = 5;";

            // Assert
            Assert.That(output == expected);
        }

        [Test]
        public void Works_MultiLiner()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTProperty_MultiLine prop = new GTProperty_MultiLine(sb))
            {
                prop.SetRequired("int", "Prop", EKeyword.PUBLIC);

                prop.Generate_DefaultValue<GTValue>().SetRequired("5");

                prop.GetFunction = new GTProperty_MultiLine.FunctionParams { IsPresent = true };

                prop.SetFunction = new GTProperty_MultiLine.FunctionParams
                {
                    IsPresent = true,
                    Statements = new string[] { "_x++;", "_x = value;" },
                    Keywords = new KeywordsCollection(EKeyword.PRIVATE)
                };
            }

            string output = sb.ToString();
            string expected = "public int Prop {\r\n   get;\r\n   private set {\r\n      _x++;\r\n      _x = value;\r\n   }\r\n} = 5;";

            // Assert
            Assert.That(output == expected);
        }

        [Test]
        public void Works_GetOnly()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTProperty_GetOnly prop = new GTProperty_GetOnly(sb))
            {
                prop.SetRequired("int", "Prop", EKeyword.PUBLIC);

                prop.Generate_DefaultValue<GTValue>().SetRequired("5");
            }

            string output = sb.ToString();
            string expected = "public int Prop => 5;";

            // Assert
            Assert.That(output == expected);
        }
    }
}