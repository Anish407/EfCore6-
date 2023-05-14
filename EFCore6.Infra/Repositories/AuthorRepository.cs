using EFCore6.Core.Entities;
using EFCore6.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EFCore6.Infra.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(PubContext context) : base(context)
        {
            Context = context;
        }

        private PubContext Context { get; }

        public async Task Add(Author author)
        {
            // search using Like or use .Contains
            // Contains will translate to Like behind the scenes 
            //Search(a => EF.Functions.Like(a.FirstName, "%A"));
            base.Add(author);
            await SaveChanges();
        }

        public async Task<Author> ExplicitLoading(Author author)
        {
            Author dbAuthor = Context.Authors.FirstOrDefault(c => c.Id == author.Id);
            await Context.Entry(dbAuthor).Collection(i=> i.Books).LoadAsync();

            return dbAuthor;
        }

        public async Task DeleteBook(Author author)
        {
            Author dbAuthor = Context.Authors.Include(a=> a.Books).FirstOrDefault(c => c.Id == author.Id);
            Book bookToDelete = dbAuthor.Books.FirstOrDefault(i => i.BookId == author.Books.FirstOrDefault().BookId);
            Context.Entry(bookToDelete).State= EntityState.Deleted;
            // use this to preview the changes that will be sent to the database.

            //we 
            var state = Context.ChangeTracker.DebugView.ShortView;
            await SaveChanges();
        }
    }
}
