using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DbMockingExtensions.ExampleDb.Models;

namespace DbMockingExtensions.ExampleDb
{
    public class ExampleDbContext : DbContext, IExampleDbContext
    {
        public ExampleDbContext() : base("exampleDbConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Revision> Revisions { get; set; }
    }
}
