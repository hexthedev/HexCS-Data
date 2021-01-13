using NUnit.Framework;
using HexCS.Core;
using HexCS.Data.Persistence;
using HexCS.Core;

namespace HexCSTests.Data
{
    [TestFixture]
    public class DataLibraryTests
    {
        [Test]
        public void Works()
        {
            // Arrange
            //PathString root = UTPathStringTests.testRoot.AddStep(nameof(DataLibraryTests));
            /*DataLibrary<ETestTypes> lib = new DataLibrary<ETestTypes>(root);

            byte[] file1 = UTRandom.Bytes(UTRandom.Int(500));
            byte[] file2 = UTRandom.Bytes(UTRandom.Int(500));
            byte[] file3 = UTRandom.Bytes(UTRandom.Int(500));

            // Act
            lib.RefreshFolders();
            lib.Clear();

            lib.WriteFile(file1, ETestTypes.Test1);
            lib.WriteFile(file2, ETestTypes.Test2);
            lib.WriteFile(file3, ETestTypes.Test3);

            lib.RefreshFiles(ETestTypes.Test2);*/

            // Assert
        }
        

        private enum ETestTypes
        {
            Test1 = 0,
            Test2 = 1,
            Test3 = 2
        }
    }
}