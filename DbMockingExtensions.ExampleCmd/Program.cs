using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbMockingExtensions.ExampleDb;

namespace DbMockingExtensions.ExampleCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() => MainAsync(args));
            Console.ReadKey();
        }

        static async void MainAsync(string[] args)
        {
            using (var context = new ExampleDbContext())
            {
                var exampleService = new ExampleService(context);

                var authorCount = await exampleService.AuthorCountAsync();
                var foundAuthor = await exampleService.FindAuthorAsync(1);
                var books = await exampleService.GetAllBooks();

                Console.WriteLine($"We Have {authorCount} Author(s)");
                Console.WriteLine($"{foundAuthor.Name} Has {foundAuthor.Books.Count} book(s)");
                Console.WriteLine($"And {books.Count} book(s) in total");
            }
        }
    }
}
