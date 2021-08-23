using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Contracts.Repositories
{
    public interface ITrackingDeviceRepository : IRepository<TrackingDevice>
    {
        Task<TrackingDevice> GetByImeiAsync(Guid Imei);
    }
}