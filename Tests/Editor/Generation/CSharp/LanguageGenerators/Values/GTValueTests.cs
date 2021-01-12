using NUnit.Framework;
using System.Text;
using HexCS.Data.Generation.CSharp;

namespace HexCSTests.Data
{
    [TestFixture]
    public class GTValueTests
    {
        [Test]
        public void Basic()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTValue value = new GTValue(sb))
            {
                value.SetRequired("7");
            }

            // Assert
            Assert.That(sb.ToString() == "7");
        }

        [Test]
        public void ArrayInitializer()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTValue_ArrayInitializer value = new GTValue_ArrayInitializer(sb))
            {
                value.SetRequired("int", true);

                for(int i = 0; i<3; i++)
                {
                    using (GTValue v = value.Generate_Value<GTValue>())
                    {
                        v.SetRequired(i.ToString());
                    }
                }
            }

            string response = sb.ToString();
            string expected = "new int[] {\r\n   0,\r\n   1,\r\n   2\r\n}";

            // Assert
            Assert.That(response == expected);
        }

        [Test]
        public void ObjectInitializer()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTValue_ObjectInitializer obj = new GTValue_ObjectInitializer(sb))
            {
                obj.SetRequired("TestObject");

                using (GTValue val = obj.Generate_NamedValue<GTValue>("Normal"))
                {
                    val.SetRequired("7");
                }

                using (GTValue_ArrayInitializer arr = obj.Generate_NamedValue<GTValue_ArrayInitializer>("Array"))
                {
                    arr.SetRequired("int");

                    using (GTValue inner = arr.Generate_Value<GTValue>())
                    {
                        inner.SetRequired("5");
                    }
                }
            }

            string response = sb.ToString();
            string expected = "new TestObject() {\r\n   Normal = 7,\r\n   Array = new int[] {\r\n      5\r\n   }\r\n}";

            // Assert
            Assert.That(response == expected);
        }
    }
}