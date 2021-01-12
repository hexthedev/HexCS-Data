using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HexCS.Data.Generation.Yaml;
using HexCS.Data.Parsing;
using HexCS.Data.Parsing.Yaml;
using HexCS.Data.Runtime;

namespace HexCSTests.Data
{
    [TestFixture]
    public class YamlGeneratorTests
    {
        [Test]
        public void ObjectListListTests() => GenericGeneratorTest(TestDefs.ObjectListListRuntime, TestDefs.ObjectListListYaml);

        [Test]
        public void ObjectListTests() => GenericGeneratorTest(TestDefs.ObjectListRuntime, TestDefs.ObjectListYaml);

        [Test]
        public void ObjectsTests() => GenericGeneratorTest(TestDefs.ObjectsRuntime, TestDefs.ObjectsYaml);

        [Test]
        public void PrimativeLists() => GenericGeneratorTest(TestDefs.PrimativeListRuntime, TestDefs.PrimativeListYaml);

        [Test]
        public void Primatives() => GenericGeneratorTest(TestDefs.PrimativeRuntime, TestDefs.PrimativeYaml);

        private void GenericGeneratorTest(IData data, string test)
        {
            // Act
            StringBuilder sb = new StringBuilder();
            using (GTIDataFile f = new GTIDataFile(sb, data)) { }

            string output = sb.ToString();

            //Assert
            Assert.That(output == test);
        }
    }
}