using System;
using Application.Features.Locations.Queries.GetVehicleCurrentPosition;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Contracts.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<VehicleCurrentLocationDto> GetCurrentPositonVehicle(int VehicleId);

        Task<List<VehiclePositionDto>> GetByDate(int VehicleId, DateTime FromDate, DateTime ToDate);
    }
}