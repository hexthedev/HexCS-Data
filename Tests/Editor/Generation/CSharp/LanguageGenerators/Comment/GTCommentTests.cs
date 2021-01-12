using System.Text;
using NUnit.Framework;
using HexCS.Data.Generation.CSharp;

namespace HexCSTests.Data
{
    [TestFixture]
    public class GTCommentTests
    {
        [Test]
        public void Works()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTComment comment = new GTComment(sb))
            {
                comment.SetRequired("SummarySummary");
                comment.Returns = "ReturnReturns";
                comment.Add_Paramater(new GTComment.Parameter("PName", "PDeets"));
            }

            string output = sb.ToString();

            // Assert
            Assert.That(output == "/// <summary>\r\n/// SummarySummary\r\n/// </summary>\r\n/// <param name=\"PName\">PDeets</param>\r\n/// <returns>ReturnReturns</returns>");
        }

        [Test]
        public void Inheritdoc()
        {
            // Arrange
            StringBuilder sb = new StringBuilder();

            // Act
            using (GTComment_Inheritdoc comment = new GTComment_Inheritdoc(sb))
            {
 
            }

            string output = sb.ToString();

            // Assert
            Assert.That(output == "/// <inheritdoc />\r\n");
        }
    }
}