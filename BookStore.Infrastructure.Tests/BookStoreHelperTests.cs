using BookStore.Domain.Models;
using BookStore.Infrastructure.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Tests
{
    public static class BookStoreHelperTests
    {
        public static DbContextOptions<BookStoreDbContext> BookStoreDbContextOptionsSQLiteInMemory()
        {
            var connectionStringBuilder =
                new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());

            var options = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseSqlite(connection)
                .Options;

            return options;
        }

        public static async void CreateDataBaseSQLiteInMemory(DbContextOptions<BookStoreDbContext> options)
        {
            await using (var context = new BookStoreDbContext(options))
            {
                await context.Database.OpenConnectionAsync();
                await context.Database.EnsureCreatedAsync();
                CreateData(context);
            }
        }

        private static void CreateData(BookStoreDbContext bookStoreDbContext)
        {
            bookStoreDbContext.Categories.Add(new Category { Id = 1, Name = "Category 1" });
            bookStoreDbContext.Categories.Add(new Category { Id = 2, Name = "Category 2" });
            bookStoreDbContext.Categories.Add(new Category { Id = 3, Name = "Category 3" });
            bookStoreDbContext.Books.Add(new Book()
            {
                Id = 1,
                Name = "Book Test 1",
                Author = "Author Test 1",
                Description = "Description Test 1",
                Value = 10,
                CategoryId = 1,
                PublishDate = new DateTime(2020, 1, 1, 0, 0, 0, 0)
            });
            bookStoreDbContext.Books.Add(new Book()
            {
                Id = 2,
                Name = "Book Test 2",
                Author = "Author Test 2",
                Description = "Description Test 2",
                Value = 20,
                CategoryId = 1,
                PublishDate = new DateTime(2020, 2, 2, 0, 0, 0, 0)
            });
            bookStoreDbContext.Books.Add(new Book()
            {
                Id = 3,
                Name = "Book Test 3",
                Author = "Author Test 3",
                Description = "Description Test 3",
                Value = 30,
                CategoryId = 3,
                PublishDate = new DateTime(2020, 3, 3, 0, 0, 0, 0)
            });

            bookStoreDbContext.SaveChangesAsync();
        }
        public static async Task CleanDataBase(DbContextOptions<BookStoreDbContext> options)
        {
            await using (var context = new BookStoreDbContext(options))
            {
                foreach (var book in context.Books)
                    context.Books.Remove(book);
                await context.SaveChangesAsync();
            }

            await using (var context = new BookStoreDbContext(options))
            {
                foreach (var category in context.Categories)
                    context.Categories.Remove(category);
                await context.SaveChangesAsync();
            }
        }
    }
}
