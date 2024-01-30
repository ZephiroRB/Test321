using System.Linq.Expressions;

namespace CleanMultitenant.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>>? expression);
        Task<T?> FirstAsync(Expression<Func<T, bool>>? expression);
    }
}
