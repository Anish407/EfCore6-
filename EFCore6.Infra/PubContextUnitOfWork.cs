using EFCore6.Core.Repositories;
using EFCore6.Infra.Repositories;
using Microsoft.Extensions.Configuration;

namespace EFCore6.Infra
{
    public class PubContextUnitOfWork : IPubContextUnitOfWork
    {
        public PubContextUnitOfWork(PubContext pubContext)
        {
            PubContext = pubContext;
        }

        public PubContext PubContext { get; }

        public IAuthorRepository AuthorRepository => new AuthorRepository(PubContext);

        public IBooksRepository BooksRepository => new BooksRepository(PubContext);

        public ICoversRepository CoversRepository => new CoversRepository(PubContext);

        public IArtistRepository ArtistRepository => new ArtistRepository(PubContext);

        public string State() => PubContext.ChangeTracker.DebugView.ShortView;

        public async Task SaveChanges()
        {
            await PubContext.SaveChangesAsync();
        }
    }
}
