using EFCore6.Core.Entities;

namespace EFCore6.Core.Servics.Interfaces
{
    public interface IAuthorService
    {
        void EnsureCreated();
        Task Update(Author updatedAuthor);
        Task Add(Author author);
        Task<IEnumerable<Author>> GetAll();
        Task<Author> ExplicitLoading(Author updatedAuthor);

        Task DeleteBook(Author author);

        Task AddBookUpdateAuthor(Author updatedAuthor, Book book);
    }
}