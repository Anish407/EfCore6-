using EFCore6.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCore6.Infra
{
    public class PubContext: DbContext
    {
        //public PubContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public PubContext(DbContextOptions<PubContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("PubContext"));
        //}

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books  { get; set; }
        public DbSet<Cover> Covers  { get; set; }
        public DbSet<Artist> Artists  { get; set; }
        public IConfiguration Configuration { get; }
    }
}
