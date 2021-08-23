using Application.Features.Locations.Queries.GetVehicleCurrentPosition;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Contracts.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<VehicleCurrentLocationDto> Get(int VehicleId);

        Task<List<VehiclePositionDto>> GetByDate(int VehicleId);
    }
}