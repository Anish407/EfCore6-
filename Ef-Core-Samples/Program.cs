// See https://aka.ms/new-console-template for more information
using EFCore6.Core.Entities;
using EFCore6.Infra;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");


DbContextOptionsBuilder<PubContext> dbContextOptions = new DbContextOptionsBuilder<PubContext>()
    .UseSqlServer("Server=LAPTOP-FSA8LOOJ\\SQLEXPRESS;Database=PubDatabase;Trusted_Connection=True;TrustServerCertificate=Yes");

await EnsureDatabaseCreated(dbContextOptions);
await AddAuthors();

await GetAuthors();

async Task GetAuthors()
{
    using (var context= new PubContext(dbContextOptions.Options))
    {
        var authors = await context.Authors.Include(i=> i.Books).ToListAsync();
    }
}

Console.ReadKey();


await EnsureDatabaseCreated(dbContextOptions);

async Task AddAuthors()
{
    Author author = new Author()
    {
        FirstName = "Anish",
        LastName = "Aravind",
        Books = new List<Book>()
        {
            new Book()
            {
                PublishDate = DateTime.Now,
                BasePrice = 10,
                Title = "C#"
            },
            new Book()
            {
                PublishDate = DateTime.Now,
                BasePrice = 20,
                Title = "EF CORE 6"
            }
        }
    };

    using (var context = new PubContext(dbContextOptions.Options))
    {
        await context.Authors.AddAsync(author);
        await context.SaveChangesAsync();
    }
}

static async Task EnsureDatabaseCreated(DbContextOptionsBuilder<PubContext> dbContextOptions)
{
    using (var context = new PubContext(dbContextOptions.Options))
    {
        // check if the database is created, else create it
        await context.Database.EnsureCreatedAsync();
    };
}