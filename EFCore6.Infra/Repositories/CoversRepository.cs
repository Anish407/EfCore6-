using EFCore6.Core.Entities;
using EFCore6.Core.Repositories;

namespace EFCore6.Infra.Repositories
{
    public class CoversRepository : Repository<Cover>, ICoversRepository
    {
        public CoversRepository(PubContext context) : base(context)
        {
        }
    }
}
