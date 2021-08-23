using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Contracts.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int Id);

        Task<IReadOnlyList<T>> ListAllAsync();

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size);

        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
    }
}