using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Moq.Language.Flow;

namespace DbMockingExtensions
{
    public static class AsyncDbContextExtensions
    {
        public static void WireUpAsyncDbMoq<TContext, TSet>(this ISetup<TContext, DbSet<TSet>> setup, IQueryable<TSet> fakeData) where TSet : class where TContext : class
        {
            var bookSetMock = new Mock<DbSet<TSet>>();
            bookSetMock.As<IDbAsyncEnumerable<TSet>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<TSet>(fakeData.GetEnumerator()));

            bookSetMock.As<IQueryable<TSet>>()
               .Setup(m => m.Provider)
               .Returns(new TestDbAsyncQueryProvider<TSet>(fakeData.Provider));

            bookSetMock.As<IQueryable<TSet>>().Setup(m => m.Expression).Returns(fakeData.Expression);
            bookSetMock.As<IQueryable<TSet>>().Setup(m => m.ElementType).Returns(fakeData.ElementType);
            bookSetMock.As<IQueryable<TSet>>().Setup(m => m.GetEnumerator()).Returns(fakeData.GetEnumerator());
            bookSetMock.Setup(x => x.Include(It.IsAny<string>()))
                .Returns(bookSetMock.Object);

            setup.Returns(bookSetMock.Object);
        }
    }
}
