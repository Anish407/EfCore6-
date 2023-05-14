using System.Linq.Expressions;

namespace EFCore6.Core.Repositories
{
    public interface IRepository<T> where T: class
    {
        void CheckIfDbExists();
        void Add(T entity);
        void AddRange(IEnumerable<T> entity);
        void Update(T updateEntity);
        void UpdateRange(IEnumerable<T> entity);
        Task<IEnumerable<T>> Search(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task SaveChanges();
    }
}
