using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using HexCS.Core;
using HexCS.Data.Parsing;
using HexCS.Data.Parsing.Yaml;
using HexCS.Data.Runtime;

namespace HexCSTests.Data
{
    [TestFixture]
    public class YamlParserTests
    {
        private YamlParser Parser => new YamlParser();

        [Test]
        public void ObjectsListList() => GenericGeneratorTest(TestDefs.ObjectListListYaml, TestDefs.ObjectListListRuntime);

        [Test]
        public void ObjectsList() => GenericGeneratorTest(TestDefs.ObjectListYaml, TestDefs.ObjectListRuntime);

        [Test]
        public void Objects() => GenericGeneratorTest(TestDefs.ObjectsYaml, TestDefs.ObjectsRuntime);

        [Test]
        public void PrimativeList() => GenericGeneratorTest(TestDefs.PrimativeListYaml, TestDefs.PrimativeListRuntime);

        [Test]
        public void Primatives() => GenericGeneratorTest(TestDefs.PrimativeYaml, TestDefs.PrimativeRuntime);

        private void GenericGeneratorTest(string data, IData test)
        {
            // Act
            InterObject ob = Parser.Interpret(data);
            //Assert
            Assert.That(ob.Equals(test.ConvertToIntermediate()));
        }
    }
}