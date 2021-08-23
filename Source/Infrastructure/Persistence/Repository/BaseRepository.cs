using Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly VehicleTrackingSystemDbContext _vtsDbContext;

        public BaseRepository(VehicleTrackingSystemDbContext vtsDbContext)
        {
            _vtsDbContext = vtsDbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _vtsDbContext.Set<T>().AddAsync(entity);
            await _vtsDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _vtsDbContext.Set<T>().Remove(entity);
            await _vtsDbContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            return await _vtsDbContext.Set<T>().FindAsync(Id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _vtsDbContext.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _vtsDbContext.Entry(entity).State = EntityState.Modified;
            await _vtsDbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size)
        {
            return await _vtsDbContext.Set<T>().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _vtsDbContext.Set<T>().Where(predicate).AsEnumerable<T>();
        }
    }
}