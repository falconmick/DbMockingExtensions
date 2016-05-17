using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using DbMockingExtensions.ExampleDb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DbMockingExtensions.ExampleDb.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var author1 = new Author
            {
                Id = 1,
                Name = "Somebody"
            };

            var author2 = new Author
            {
                Id = 2,
                Name = "Otherbody"
            };

            var books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "Some Amazing Book",
                    Author = author1
                },
                new Book
                {
                    Id = 2,
                    Title = "WooooW",
                    Author = author2
                },
                new Book
                {
                    Id = 3,
                    Title = "Finally!",
                    Author = author1
                }
            }.AsQueryable();

            author1.Books = new List<Book> { books.First(b => b.Id == 1), books.First(b => b.Id == 3) };
            author2.Books = new List<Book> { books.First(b => b.Id == 2) };


            var authors = new List<Author> { author1, author2 }.AsQueryable();

            //var bookSetMock = new Mock<DbSet<Book>>();
            //bookSetMock.As<IDbAsyncEnumerable<Book>>()
            //    .Setup(m => m.GetAsyncEnumerator())
            //    .Returns(new TestDbAsyncEnumerator<Book>(books.GetEnumerator()));

            //bookSetMock.As<IQueryable<Book>>()
            //   .Setup(m => m.Provider)
            //   .Returns(new TestDbAsyncQueryProvider<Book>(books.Provider));

            //bookSetMock.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(books.Expression);
            //bookSetMock.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(books.ElementType);
            //bookSetMock.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(books.GetEnumerator());

            //var mockContext = new Mock<IExampleDbContext>();
            //mockContext.Setup(c => c.Books).Returns(bookSetMock.Object);

            var mockContext = new Mock<IExampleDbContext>();
            mockContext.Setup(m => m.Books).WireUpAsyncDbMoq(books);
            mockContext.Setup(m => m.Authors).WireUpAsyncDbMoq(authors);
            
            var exampleService = new ExampleService(mockContext.Object);

            var asyncIntTasks = Task.Run(() => exampleService.AuthorCountAsync());
            var authorCount = asyncIntTasks.Result;

            var asyncAuthorTasks = Task.Run(() => exampleService.FindAuthorAsync(1));
            var foundAuthor = asyncAuthorTasks.Result;

            var asyncBookTask = Task.Run(() => exampleService.GetAllBooks());
            var allBooks = asyncBookTask.Result;

            Assert.AreEqual(authorCount, 2);
            Assert.AreEqual(foundAuthor.Name, "Somebody");
            Assert.AreEqual(foundAuthor.Books.Count, 2);
            Assert.AreEqual(allBooks.Count, 3);
        }
    }
}
