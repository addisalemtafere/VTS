using Application.Contracts.Repositories;
using Domain.Entities;
using Persistence;
using Persistence.Repository;

namespace Infrastructure.Persistence.Repository
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(VehicleTrackingSystemDbContext vtsDbContext) : base(vtsDbContext)
        {
        }
    }
}