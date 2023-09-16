using EFCore6.Core.Entities;
using EFCore6.Infra;
using Microsoft.EntityFrameworkCore;

DbContextOptionsBuilder<PubContext> dbContextOptions = new DbContextOptionsBuilder<PubContext>()
    .UseSqlServer("Server=LAPTOP-FSA8LOOJ\\SQLEXPRESS;Database=PubDatabase;Trusted_Connection=True;TrustServerCertificate=Yes")
    .LogTo(Console.WriteLine, new string[]
    { 
        // add filter to restrict the data that is being logged.
        DbLoggerCategory.Database.Command.Name,
        //DbLoggerCategory.Query.Name
    });
await CheckTransactionWorking(true);// we throw an exception and ensure that the changes are not saved to the database
await CheckTransactionWorking(false);// we commit and ensure that the changes are reflected in the database

Console.ReadLine();

async Task CheckTransactionWorking(bool throwError = false)
{
    using (var context = new PubContext(dbContextOptions.Options))
    {
        using var transaction = context.Database.BeginTransaction();
        try
        {
            context.ArtistCovers.Add(new ArtistCover { ArtistsArtistId = 1, CoversCoverId = 3 });
            context.SaveChanges(); // the data will not be saved to the database unless the transaction comits

            var lastArtist = await context.Artists.FirstOrDefaultAsync(i=> i.ArtistId==4);
            context.Artists.Remove(lastArtist);
            context.SaveChanges();

            if (throwError) throw new Exception();

            transaction.Commit(); // this is where the changes are made to the database
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync("Exception thrown"); ;
        }
        // when we retrieve the artist, we find that each cover has a list of artists in it 
        // and each artist has again a nested list of covers. That doesn't mean that it has a nested 
        // collection of objects, all these nested object just point to the same memory location and no
        // new memory is allocated for these nested objects.
        var artist = context.Artists.Include(i => i.Covers).FirstOrDefault();
    }
}