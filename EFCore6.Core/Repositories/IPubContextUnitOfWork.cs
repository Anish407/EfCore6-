using EFCore6.Core.Repositories;

namespace EFCore6.Core.Repositories
{
    public interface IPubContextUnitOfWork
    {
        IAuthorRepository AuthorRepository { get; }
        IBooksRepository BooksRepository { get; }
        ICoversRepository CoversRepository { get; }
        IArtistRepository ArtistRepository { get; }
        string State();
        Task SaveChanges();
    }
}