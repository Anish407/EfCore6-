using EFCore6.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EFCore6.Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(DbContext context)
        {
            Context = context;
        }

        private DbContext Context { get; }

        public void Add(T entity)
        {
           Context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entity)
        {
            Context.Set<T>().AddRangeAsync(entity);
        }

        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entity)
        {
            Context.Set<T>().UpdateRange(entity);
        }

        public void CheckIfDbExists()
        {
            Context.Database.EnsureCreated();
        }

        public async Task SaveChanges()
        {
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate)
        {
          return  await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().FirstOrDefaultAsync(predicate) ?? throw new Exception("Not found");
        }

    }
}
