using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistance
{
    public interface IAsyncRepository<T> where T: EntityBase
    {
        public Task<IReadOnlyList<T>> GetAllAsync();

        public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null);

        public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T,bool>> predicate = null,
                                         Func<IQueryable<T>, IOrderedQueryable<T>> OrderedBy = null,
                                         string IncludeString = null,
                                         bool DisableTracking = true);
        public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                         Func<IQueryable<T>, IOrderedQueryable<T>> OrderedBy = null,
                                         Expression<List<Func<T, object>>> Include = null,
                                         bool DisableTracking = true);

        public Task<T> GetByIdAsync(int Id);
        public Task<T> AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
    }
}
