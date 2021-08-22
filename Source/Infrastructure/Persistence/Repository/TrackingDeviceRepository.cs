using Application.Contracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repository;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class TrackingDeviceRepository : BaseRepository<TrackingDevice>, ITrackingDeviceRepository
    {
        public TrackingDeviceRepository(VehicleTrackingSystemDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<TrackingDevice> GetByImeiAsync(Guid Imei)
        {
            return await _vtsDbContext.TrackingDevices.FirstOrDefaultAsync(a => a.Imei == Imei);
        }
    }
}