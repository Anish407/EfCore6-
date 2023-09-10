using EFCore6.Core.Entities;
using EFCore6.Infra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

DbContextOptionsBuilder<PubContext> dbContextOptions = new DbContextOptionsBuilder<PubContext>()
    .UseSqlServer("Server=LAPTOP-FSA8LOOJ\\SQLEXPRESS;Database=PubDatabase;Trusted_Connection=True;TrustServerCertificate=Yes")
    .LogTo(Console.WriteLine, new string[]
    { 
        // add filter to restrict the data that is being logged.
        DbLoggerCategory.Database.Command.Name,
        //DbLoggerCategory.Query.Name
    });
try
{

    //await CreateArtistsAndCovers();

    //await AddCoverToArtist();

   await GetArtistAndItsCovers();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

async Task GetArtistAndItsCovers()
{
    using (var context = new PubContext(dbContextOptions.Options))
    {
        context.ArtistCovers.Add(new ArtistCover { ArtistsArtistId=1, CoversCoverId=3 });

        context.SaveChanges();
        // when we retrieve the artist, we find that each cover has a list of artists in it 
        // and each artist has again a nested list of covers. That doesn't mean that it has a nested 
        // collection of objects, all these nested object just point to the same memory location and no
        // new memory is allocated for these nested objects.
       var artist= context.Artists.Include(i=> i.Covers).FirstOrDefault();
    }
}

async Task AddCoverToArtist()
{
    using (var context = new PubContext(dbContextOptions.Options))
    {
        var cover1 = context.Covers.Find(1);
        var cover2 = context.Covers.Find(2);
       
        // find an existing artist
        var artist1 = context.Artists.Find(1);

        artist1.Covers.AddRange(new List<Cover> { cover1, cover2 });

        await context.SaveChangesAsync();
    }

    // Generated Query
    //INSERT INTO[ArtistCover] ([ArtistsArtistId], [CoversCoverId])
    //  VALUES(@p0, @p1),
    //  (@p2, @p3);
}

async Task CreateArtistsAndCovers()
{
    var artists = new List<Artist>
    {
        new Artist
        {
            FirstName = "Artist1",
            LastName= "Artist1"
        },
        new Artist
        {
            FirstName = "Artist2",
            LastName= "Artist2"
        }
    };

    var covers = new List<Cover>
    {
        new Cover
        {
            DesignIdeas="Cover1",
            DigitalOnly=true,
        },
        new Cover
        {
            DesignIdeas="Cover2",
            DigitalOnly=false,
        },
        new Cover
        {
            DesignIdeas="Cover3",
            DigitalOnly=true,
        }
    };

    try
    {
        using (var context = new PubContext(dbContextOptions.Options))
        {
            await context.Covers.AddRangeAsync(covers);
            await context.Artists.AddRangeAsync(artists);

            await context.SaveChangesAsync();
        }
    }
    catch (Exception e)
    {
        await Console.Out.WriteLineAsync(e.Message);
    }
}