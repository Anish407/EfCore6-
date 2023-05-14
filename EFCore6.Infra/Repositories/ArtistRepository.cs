using EFCore6.Core.Entities;
using EFCore6.Core.Repositories;

namespace EFCore6.Infra.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(PubContext context) : base(context)
        {
        }
    }
}
