using EFCore6.Core.Entities;
using EFCore6.Core.Repositories;
using EFCore6.Core.Servics.Interfaces;

namespace EFCore6.Core.Servics.Implemetations
{
    public class AuthorService : IAuthorService
    {
        public AuthorService(IPubContextUnitOfWork pubContextUnitOfWork)
        {
            PubContextUnitOfWork = pubContextUnitOfWork;
            AuthorsRepository = PubContextUnitOfWork.AuthorRepository;

        }

        public IAuthorRepository AuthorsRepository { get; }
        public IPubContextUnitOfWork PubContextUnitOfWork { get; }

        public async Task Add(Author author)
        {
            try
            {
                AuthorsRepository.Add(author);
                await PubContextUnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Update(Author updatedAuthor)
        {
            try
            {
                var updateAuthor = await AuthorsRepository.FirstOrDefault(i => i.Id == updatedAuthor.Id);
                updateAuthor.FirstName = updatedAuthor.FirstName;
                updateAuthor.LastName = updatedAuthor.LastName;
                updateAuthor.Books = updatedAuthor.Books;

                //AuthorsRepository.Update(updateAuthor);
                await PubContextUnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteBook(Author updatedAuthor)
        {
            try
            {
                await AuthorsRepository.DeleteBook(updatedAuthor);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Author> ExplicitLoading(Author updatedAuthor)
        {
            try
            {
                return await AuthorsRepository.ExplicitLoading(updatedAuthor);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AddBookUpdateAuthor(Author updatedAuthor, Book book)
        {
            try
            {
                var bookRepository = PubContextUnitOfWork.BooksRepository;
                bookRepository.Add(book);

                var updateAuthor = await AuthorsRepository.FirstOrDefault(i => i.Id == updatedAuthor.Id);
                updateAuthor.Books.Add(book);

                // Update will set the entity state to Modified and generate an update sql command
                AuthorsRepository.Update(updateAuthor);
                await PubContextUnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EnsureCreated()
        {
            AuthorsRepository.CheckIfDbExists();
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await AuthorsRepository.Search(author => true);
        }
    }
}
