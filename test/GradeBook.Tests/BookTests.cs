using System;
using Xunit;


namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact]
        public void CalculateAverage()
        {
            var book = new InMemoryBook("");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.3);

            var stats = book.GetStatistics();

            Assert.Equal(85.6, stats.Average, 1);
            Assert.Equal(90.5, stats.Highest, 1);
            Assert.Equal(77.3, stats.Lowest, 1);
        }
    }
}
