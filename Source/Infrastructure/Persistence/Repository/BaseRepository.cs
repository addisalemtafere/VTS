using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Persisitance;

namespace VTS.Persistence.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly VehicleTrackingSystemDbContext _vtsDbContext;
        public BaseRepository(VehicleTrackingSystemDbContext vtsDbContext)
        {
            _vtsDbContext = vtsDbContext;

        }
        public async Task<T> AddAsync(T Enitity)
        {
            await _vtsDbContext.Set<T>().AddAsync(Enitity);
            await _vtsDbContext.SaveChangesAsync();
            return Enitity;
        }

        public async Task DeleteAsync(T Enitity)
        {
            _vtsDbContext.Set<T>().Remove(Enitity);
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

        public async Task UpdateAsync(T Enitity)
        {
            _vtsDbContext.Entry(Enitity).State = EntityState.Modified;
            await _vtsDbContext.SaveChangesAsync();

        }
    }
}
