// See https://aka.ms/new-console-template for more information
using Ef_Core_Samples;
using EFCore6.Core.Entities;
using EFCore6.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");


DbContextOptionsBuilder<PubContext> dbContextOptions = new DbContextOptionsBuilder<PubContext>()
    .UseSqlServer("Server=LAPTOP-FSA8LOOJ\\SQLEXPRESS;Database=PubDatabase;Trusted_Connection=True;TrustServerCertificate=Yes")
    .LogTo(Console.WriteLine, new string[] 
    { 
        // add filter to restrict the data that is being logged.
        DbLoggerCategory.Database.Command.Name,
        //DbLoggerCategory.Query.Name
    });

//await EnsureDatabaseCreated(dbContextOptions);

//var operators = new Operators();
//await operators.RunSamples(dbContextOptions);

await EagerLoadWithRelatedData();
//Console.ReadLine();
//await UpdateAuthor();

async Task UpdateRelatedData()
{
    try
    {
        using (var context = new PubContext(dbContextOptions.Options))
        {
            var authors = await context.Authors.Include(i=> i.Books).FirstOrDefaultAsync(i => i.Id == 1);

            authors!.Books.Add(new Book
            {
                BasePrice = 50,
                PublishDate = DateTime.Now,
                Title = "Title",
            });

            // this should not be called as the author object is tracked by the 
            // context and it wud generate an insert query on the author that already 
            // exists in the database.
            // context.Authors.Add(authors);
             context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        await Console.Out.WriteLineAsync($"{ex.Message}"); ;
    }
}

async Task EagerLoadWithRelatedData()
{
    using(var context = new PubContext(dbContextOptions.Options))
    {
        DateTime expectedDate = new DateTime(2023, 08, 25);
        //var result = context.Authors
        //    .Include(i => i.Books.Where(i => i.PublishDate.Date >= expectedDate)).ToList();


        // whatever comes after AsEnumerable will be executed in memory
        // Generated Query
        //SELECT[a].[Id], [a].[FirstName], [a].[LastName]
        //  FROM[Authors] AS[a]
        //  WHERE[a].[FirstName] = N'Anish' AND[a].[Id] = 1
       var result2= context.Authors
            .Where(i=> i.FirstName== "Anish").Where(i => i.Id == 1)
            .AsEnumerable()
            .Where(i=> i.Id==2).ToList();

    }
}

//await AddAuthors();

//await GetAuthors();

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