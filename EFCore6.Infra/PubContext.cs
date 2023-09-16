using EFCore6.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCore6.Infra
{
    public class PubContext : DbContext
    {
        //public PubContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public PubContext(DbContextOptions<PubContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // in our app we will pass the connection string , but while testing we can 
            // check if the context has not been configured then we can 
            // connect to the local database
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Connection string for the local database");
            }
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Cover> Covers { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<ArtistCover> ArtistCovers { get; set; }
        public IConfiguration Configuration { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>()
                .HasMany(i => i.Covers)
                .WithMany(i => i.Artists)
                .UsingEntity<ArtistCover>();
            modelBuilder.Entity<ArtistCover>()
                .ToTable("ArtistCover")
                .HasKey(i => new { i.ArtistsArtistId, i.CoversCoverId });
        }
    }
}
