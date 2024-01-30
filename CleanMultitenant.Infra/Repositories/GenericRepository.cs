using CleanMultitenant.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanMultitenant.Infra.Repositories
{
    public abstract class GenericRepository<TContext, T> : IGenericRepository<T> where T : class where TContext : DbContext
    {
        protected TContext _context;
        private readonly DbSet<T> _dbSet;

        protected GenericRepository(TContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>>? expression)
        {
            var query = _dbSet.AsNoTrackingWithIdentityResolution().AsQueryable();

            if (expression != null)
                query = query.Where(expression);

            return await query.ToListAsync();
            
        }

        public async Task<T?> FirstAsync(Expression<Func<T, bool>>? expression)
        {
            var query = _dbSet.AsNoTrackingWithIdentityResolution().AsQueryable();

            if (expression != null)
                query = query.Where(expression);

            return await query.FirstOrDefaultAsync();
        }
    }
}
