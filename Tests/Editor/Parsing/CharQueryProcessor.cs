using NUnit.Framework;
using HexCS.Core;

namespace HexCSTests.Data
{
    [TestFixture]
    public class CharQueryProcessorTests
    {
        [Test]
        public void Works()
        {
            // Arrange
            CharQueryProcessor<int> proc = new CharQueryProcessor<int>();

            proc.SkipTests = new DCharQuery[] { UTCharQuery.GetOccurenceOfQuery('a') };

            proc.AnalysisInstructions = new CharQueryMapping<int>[]
            {
                new CharQueryMapping<int>() { Id = 0, Query = UTCharQuery.GetOccurenceOfQuery('a') }, // should never get picked up 
                new CharQueryMapping<int>() { Id = 1, Query = UTCharQuery.GetQuery(ECharQuery.NewLine)},
                new CharQueryMapping<int>() { Id = 2, Query = UTCharQuery.GetOccurenceOfQuery('b') }
            };
                        
            // indices:    012-3-456-789
            string test = "aa\r\naa\nbab";

            // Act
            CharQueryAnalysisUnit<int>[] analysis = proc.Analyze(ref test);

            // Assert
            Assert.That(
                analysis.Length == 4
                && analysis[0].Id == 1 && analysis[0].Index == 2
                && analysis[1].Id == 1 && analysis[1].Index == 6
                && analysis[2].Id == 2 && analysis[2].Index == 7
                && analysis[3].Id == 2 && analysis[3].Index == 9
            );
        }
    }
}   