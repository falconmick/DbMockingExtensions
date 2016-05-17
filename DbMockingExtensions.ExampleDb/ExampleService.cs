using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbMockingExtensions.ExampleDb.Models;

namespace DbMockingExtensions.ExampleDb
{
    public interface IExampleService
    {
        Task<int> AuthorCountAsync();
        Task<Author> FindAuthorAsync(int id);
        Task<List<Book>> GetAllBooks();
    }

    public class ExampleService : IExampleService
    {
        private readonly IExampleDbContext _context;
        public ExampleService(IExampleDbContext context)
        {
            _context = context;
        }

        public async Task<int> AuthorCountAsync()
        {
            return await _context.Authors.CountAsync();
        }

        public async Task<Author> FindAuthorAsync(int id)
        {
            //return await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }
    }
}
