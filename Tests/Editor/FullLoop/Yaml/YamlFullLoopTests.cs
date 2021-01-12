using NUnit.Framework;
using System.Text;
using HexCS.Data.Encryption;
using HexCS.Data.Generation.Yaml;
using HexCS.Data.Parsing.Yaml;
using HexCS.Data.Runtime;

namespace HexCSTests.Data
{
    [TestFixture]
    public class YamlFullLoopTests
    {
        private YamlParser Parser => new YamlParser();

        [Test]
        public void ObjectsListList() => GenericFullLoopTest(TestDefs.ObjectListListRuntime);

        [Test]
        public void ObjectsList() => GenericFullLoopTest(TestDefs.ObjectListRuntime);

        [Test]
        public void Objects() => GenericFullLoopTest(TestDefs.ObjectsRuntime);

        [Test]
        public void PrimativeList() => GenericFullLoopTest(TestDefs.PrimativeListRuntime);

        [Test]
        public void Primatives() => GenericFullLoopTest(TestDefs.PrimativeRuntime);

        private void GenericFullLoopTest(IData data)
        {
            EncryptorAes aes = EncryptionHelpers.MakeEncryptor();
            StringBuilder sb = new StringBuilder();

            using (GTIDataFile f = new GTIDataFile(sb, data)) { }
            string output = sb.ToString();

            byte[] encrypted = aes.Encrypt(Encoding.Unicode.GetBytes(output));
            byte[] decrypted = aes.Decrypt(encrypted);

            string cyptoOut = Encoding.Unicode.GetString(decrypted);

            // Act
            InterObject ob = Parser.Interpret(cyptoOut);
            //Assert
            Assert.That(ob.Equals(data.ConvertToIntermediate()));
        }
    }
}