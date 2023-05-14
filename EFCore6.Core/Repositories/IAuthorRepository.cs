using EFCore6.Core.Entities;

namespace EFCore6.Core.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author> ExplicitLoading(Author author);

        Task DeleteBook(Author author);
    }
}
