using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repository;

namespace Infrastructure.Persistence.Repository
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(VehicleTrackingSystemDbContext vtsDbContext) : base(vtsDbContext)
        {
        }

        public async Task<List<Vehicle>> GetPagedVehicle(int page, int size)
        {
            return await _vtsDbContext.Vehicles.Include(p => p.TrackingDevice)
                .Skip((page - 1) * size).Take(size)
                .AsNoTracking().ToListAsync();
        }

        public async Task<int> countVehicle()
        {
            return _vtsDbContext.Vehicles.Select(x => x.Id).Count();
        }
    }
}