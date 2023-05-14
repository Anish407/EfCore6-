using EFCore6.Core.Entities;
using EFCore6.Core.Repositories;

namespace EFCore6.Infra.Repositories
{
    public class BooksRepository : Repository<Book>, IBooksRepository
    {
        public BooksRepository(PubContext context) : base(context)
        {
        }
    }
}
