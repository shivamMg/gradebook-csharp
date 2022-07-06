using System;
using Xunit;


namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string logMessage);

    public class TypeTests
    {
        [Fact]
        public void TestWriteLogDelegate()
        {
            WriteLogDelegate log = ReturnUpper;
            log += ReturnLower;
            var result = log("Hello");  // Calls both ReturnUpper and ReturnLower in that order

            Assert.Equal("hello", result);
        }

        public string ReturnUpper(string logMessage)
        {
            return logMessage.ToUpper();
        }

        public string ReturnLower(string logMessage)
        {
            return logMessage.ToLower();
        }

        [Fact]
        public void TestPassByValueInt()
        {
            var x = GetAnswerToLifeꓹTheUniverseAndEverything();
            x++;
            Assert.Equal(43, x);
        }

        [Fact]
        public void TestPassByReference()
        {
            var book1 = GetBook("Book 1");
            GetBookAndSetName(ref book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        [Fact]
        public void TestPassByValue()
        {
            var book1 = GetBook("Book 1");
            GetBookAndSetName(book1, "New Name");

            Assert.Equal("Book 1", book1.Name);
        }

        [Fact]
        public void TestSetName()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        [Fact]
        public void TestDifferentBooks()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);
        }

        [Fact]
        public void TestSameBooks()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 1", book2.Name);
            Assert.Same(book1, book2);
            Assert.True(object.ReferenceEquals(book1, book2));
        }

        private InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }

        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        private void GetBookAndSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        private void GetBookAndSetName(ref InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        private int GetAnswerToLifeꓹTheUniverseAndEverything()
        {
            return 42;
        }
    }
}
