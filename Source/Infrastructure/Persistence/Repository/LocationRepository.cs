using Application.Contracts.Repositories;
using Application.Features.Locations.Queries.GetVehicleCurrentPosition;
using Domain.Entities;
using Infrastructure.Persistence.DataProvider;
using Persistence;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public LocationRepository(VehicleTrackingSystemDbContext dbContext,
            ISqlConnectionFactory sqlConnectionFactory) : base(dbContext)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<VehicleCurrentLocationDto> GetCurrentPositionVehicle(int VehicleId)
        {
            var locationResponse =
                await LocationDataProvider.GetVehicleLocation(_sqlConnectionFactory.GetOpenConnection(), VehicleId);
            return locationResponse;
        }

        public async Task<List<VehiclePositionDto>> GetByDate(int VehicleId, DateTime FromDate, DateTime ToDate)
        {
            var locationResponse =
                await LocationDataProvider.GetVehicleLocationByDate(_sqlConnectionFactory.GetOpenConnection(),
                    VehicleId, FromDate, ToDate);
            return locationResponse;
        }
    }
}