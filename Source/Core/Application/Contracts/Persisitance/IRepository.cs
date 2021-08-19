using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persisitance
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int Id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T Enitity);
        Task UpdateAsync(T Enitity);
        Task DeleteAsync(T Enitity);
    }
}
