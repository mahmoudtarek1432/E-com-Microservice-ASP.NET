using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repository
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
    {
        protected readonly OrderContext _Context;
        public RepositoryBase(OrderContext dbContext) 
        {
            _Context = dbContext?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<T> AddAsync(T entity)
        {
            _Context.Set<T>().Add(entity);
            await _Context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _Context.Set<T>().Remove(entity);
            await _Context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _Context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await _Context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> OrderedBy = null, string IncludeString = null, bool DisableTracking = true)
        {
            IQueryable<T> query = _Context.Set<T>();
            if(predicate != null) query = query.Where(predicate);

            if(OrderedBy != null) await OrderedBy(query).ToListAsync();

            if(!string.IsNullOrWhiteSpace(IncludeString)) query = query.Include(IncludeString);

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> OrderedBy = null, Expression<List<Func<T, object>>> Include = null, bool DisableTracking = true)
        {
            IQueryable<T> query = _Context.Set<T>();
            if (predicate != null) query = query.Where(predicate);

            if (OrderedBy != null) return await OrderedBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int Id)
        {
            return await _Context.Set<T>().FindAsync(Id);
        }

        public async Task UpdateAsync(T entity)
        {
            _Context.Set<T>().Update(entity);
            await _Context.SaveChangesAsync();
        }
    }
}
