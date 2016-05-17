using System.Data.Entity;
using System.Threading.Tasks;
using DbMockingExtensions.ExampleDb.Models;

namespace DbMockingExtensions.ExampleDb
{
    public interface IExampleDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}