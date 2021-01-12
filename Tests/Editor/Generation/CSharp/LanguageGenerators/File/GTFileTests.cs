using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using HexCS.Data.Generation.CSharp;
using HexCS.Data.Persistence;
using Hex.Paths;

namespace HexCSTests.Data
{
    [TestFixture]
    public class GTFileTests
    {
        private const string _tempFolderName = "TobiasCS/Tests/GTFileTests";

        private PathString _tempFolder = UTCommonPaths.AppDataPath.AddStep(_tempFolderName);

        [Test]
        public void Works()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            PathString path = _tempFolder.AddStep("GTFileTestsWorks.txt");

            // Act
            using (GTFile f = new GTFile(sb, path, Encoding.UTF8))
            {
                f.Generate_Usings<GTUsings>().SetRequired("Test.OK", "System.Happening");
                f.Generate_Namespace<GTNamespace>().SetRequired("System.Test");
            }

            string output = sb.ToString();
            string fileOutput = string.Empty;

            if(path.TryAsFileInfo(out FileInfo info))
            {
                fileOutput = info.ReadAllText();
            }

            string expected = "using Test.OK;\r\nusing System.Happening;\r\n\r\nnamespace System.Test\r\n{\r\n\r\n}";

            // Assert
            Assert.That(output == expected);
            Assert.That(fileOutput == expected);

            if(_tempFolder.TryAsDirectoryInfo(out DirectoryInfo dirInfo))
            {
                dirInfo.Delete(true);
            }
        }

        // Generate a file that contains an abstract class and an enum
        [Test]
        public void WorksComplexe()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            PathString path = _tempFolder.AddStep("GTFileTestsWorksComplexe.txt");

            // Act
            using (GTFile f = new GTFile(sb, path, Encoding.UTF8))
            {
                f.Generate_Usings<GTUsings>().SetRequired("Test.OK1", "Test.OK2");

                using (GTNamespace nsp = f.Generate_Namespace<GTNamespace>())
                {
                    nsp.SetRequired("Test.Complex");
                    nsp.Generate_Comment<GTComment>().Summary = "Namespace";

                    using (GTClass cls = nsp.Generate_NamespaceObject<GTClass>())
                    {
                        cls.Generate_Comment<GTComment>().Summary = "Class";

                        using (GTAttribute atr = cls.Generate_Attribute<GTAttribute>())
                        {
                            atr.SetRequired("TestAttr");
                            atr.Add_Args( new Arg_Named("name", "3"));
                        }

                        cls.SetRequired("AClass", EKeyword.PUBLIC, EKeyword.ABSTRACT);
                        cls.Add_GenericTypes( "T1", "T2");
                        cls.Add_GenericTypeConstraints("T1 : T2");

                        cls.Generate_Field<GTField>().SetRequired("float", "_field1", EKeyword.PRIVATE);

                        using (GTFunction_Abstract funca = cls.Generate_Function<GTFunction_Abstract>())
                        {
                            using (GTComment com = funca.Generate_Comment<GTComment>())
                            {
                                com.Summary = "FuncA";
                                com.Add_Paramater( new GTComment.Parameter("x", "this is x") );
                            }

                            funca.SetRequired("void", "FuncA", EKeyword.PUBLIC);
                            funca.Add_Paramaters(new Parameter_Basic("int", "x"));
                        }

                    }

                    nsp.Generate_NamespaceObject<GTEnum>().SetRequired(
                        "Enum1",
                        new EnumValue[] { new EnumValue("E1", "1"), new EnumValue("E2", "2") },
                        EKeyword.PUBLIC
                    );
                }
            }

            string output = sb.ToString();
            string fileOutput = string.Empty;

            if (path.TryAsFileInfo(out FileInfo info))
            {
                fileOutput = info.ReadAllText();
            }

            string expected = @"using Test.OK1;
using Test.OK2;

/// <summary>
/// Namespace
/// </summary>
namespace Test.Complex
{
   /// <summary>
   /// Class
   /// </summary>
   [TestAttr(name = 3)]
   public abstract class AClass<T1, T2>
      where T1 : T2
   {
      private float _field1;

      /// <summary>
      /// FuncA
      /// </summary>
      /// <param name=""x"">this is x</param>
      public abstract void FuncA(int x);

   }

   public enum Enum1 {
      E1 = 1,
      E2 = 2
   }
}";

            // Assert
            Assert.That(output == expected);
            Assert.That(fileOutput == expected);

            if (_tempFolder.TryAsDirectoryInfo(out DirectoryInfo dirInfo))
            {
                dirInfo.Delete(true);
            }
        }
    }
}