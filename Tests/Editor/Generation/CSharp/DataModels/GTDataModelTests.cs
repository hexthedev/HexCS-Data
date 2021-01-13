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
    public class GTModelTests
    {
        private const string _tempFolderName = "TobiasCS/Tests/GTFileTests";

        private PathString _tempFolder = UTCommonPaths.AppDataPath.InsertAtEnd(_tempFolderName);

        [Test]
        public void Works()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();
            PathString path = _tempFolder.InsertAtEnd("GTFileTestsWorks.txt");

            // Act
            using (GTDataModel_Public dm = new GTDataModel_Public(sb, path, Encoding.UTF8))
            {
                dm.Generate_Usings<GTUsings>().SetRequired("Test.OK", "System.Happening");
                
                dm.Namespace = "System.TestModel";

                dm.ModelArgs = new GTDataModel_Public.Model {
                    ModelName = "ModModel",
                    Comment = "What a model",
                    Fields = new GTDataModel_Public.ModelField[]
                    {
                        new GTDataModel_Public.ModelField {
                            Type = "int",
                            Name = "Mod1",
                            Comment = "Mod1",
                            DefaultValue = "7"
                        },
                        new GTDataModel_Public.ModelField {
                            Type = "int",
                            Name = "Mod2"
                        },
                    }
                };
            }

            string output = sb.ToString();
            string fileOutput = string.Empty;

            if (path.TryAsFileInfo(out FileInfo info))
            {
                fileOutput = info.ReadAllText();
            }

            string expected = @"using Test.OK;
using System.Happening;

namespace System.TestModel
{
   /// <summary>
   /// What a model
   /// </summary>
   public class ModModel
   {
      /// <summary>
      /// Mod1
      /// </summary>
      public int Mod1 = 7;

      public int Mod2;

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