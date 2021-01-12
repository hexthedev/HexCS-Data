using System.Text;
using NUnit.Framework;
using HexCS.Data.Generation.CSharp;

namespace HexCSTests.Data
{
    [TestFixture]
    public class GTFieldTests
    {
        [Test]
        public void Works()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTField field = new GTField(sb))
            {
                field.SetRequired("int", "field");
            }
            string field_basic = sb.ToString();
            sb.Clear();

            using (GTField field = new GTField(sb))
            {
                field.SetRequired("int", "field", EKeyword.PUBLIC);
            }
            string field_accessor = sb.ToString();
            sb.Clear();

            using (GTField field = new GTField(sb))
            {
                field.SetRequired("int", "field", EKeyword.PROTECTED, EKeyword.STATIC, EKeyword.READONLY);
            }
            string field_flavour = sb.ToString();
            sb.Clear();

            using (GTField field = new GTField(sb))
            {
                field.SetRequired("int", "field", EKeyword.PROTECTED, EKeyword.STATIC);

                field.Generate_DefaultValue<GTValue>().SetRequired("3");
            }
            string field_default = sb.ToString();
            sb.Clear();

            using (GTField field = new GTField(sb))
            {
                field.Add_Keywords(
                    EKeyword.PROTECTED,
                    EKeyword.READONLY
                );
            }
            string field_error = sb.ToString();
            sb.Clear();

            // Assert
            Assert.That(field_basic == "int field;");
            Assert.That(field_accessor == "public int field;");
            Assert.That(field_flavour == "protected static readonly int field;");
            Assert.That(field_default == "protected static int field = 3;");
            Assert.That(field_error == "protected readonly ERROR_MISSING_TYPE ERROR_MISSING_NAME;");
        }
    }
}