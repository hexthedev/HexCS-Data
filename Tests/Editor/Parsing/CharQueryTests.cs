using NUnit.Framework;
using HexCS.Data.Parsing;
using HexCS.Core;

namespace HexCSTests.Data
{
    [TestFixture]
    public class CharQueryTests
    {
        /// <summary>
        /// Test string to check each char is found
        /// - windowns new line: 0, 2, 13
        /// - linux new line: 1, 3, 4, 5, 6, 12, 14
        /// - new line : 0, 2, 4, 5, 6, 12, 13
        /// - startOfLine: 7
        /// </summary>
        //                            0-1-2-3-4-5-6-789012-3-4-
        private string cTestString = "\r\n\r\n\n\n\nHello\n\r\n";

        [Test]
        public void Works()
        {
            // Arrange
            // Act
            bool NewlineTest = CharQueryTrueAtAllIndices(UTCharQuery.GetQuery(ECharQuery.NewLine), 0,2,4,5,6,12,13);
            bool NewlineWindowsTest = CharQueryTrueAtAllIndices(UTCharQuery.GetQuery(ECharQuery.NewLine_Windows), 0, 2, 13);
            bool NewlineLinuxTest = CharQueryTrueAtAllIndices(UTCharQuery.GetQuery(ECharQuery.NewLine_Linux), 1,3,4,5,6,12,14);
            bool StartOfLine = CharQueryTrueAtAllIndices(UTCharQuery.GetQuery(ECharQuery.StartOfLine), 7);

            bool OccurenceOfQuery = CharQueryTrueAtAllIndices(UTCharQuery.GetOccurenceOfQuery('l'), 9, 10);

            // Assert
            Assert.That(
                NewlineTest &&
                NewlineWindowsTest &&
                NewlineLinuxTest &&
                StartOfLine &&
                OccurenceOfQuery
            );
        }

        private bool CharQueryTrueAtAllIndices(DCharQuery query, params int[] indicies)
        {
            for(int i = 0; i < cTestString.Length; i++)
            {
                bool instanceOfQuery = query(ref cTestString, i);

                if(instanceOfQuery)
                {
                    if (!indicies.QueryContains(el => el == i)) return false;
                }
                else
                {
                    if (indicies.QueryContains(el => el == i)) return false;
                }
            }
            return true;
        }
    }
}