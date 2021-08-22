using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Locations.Queries.GetVehicleCurrentPosition;
using Domain.Entities;
using Infrastructure.Persistence.DataProvider;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repository;

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


        public async Task<VehicleCurrentLocationDto> Get(int VehicleId)
        {
            var locationResponse =
                await LocationDataProvider.GetVehicleLocation(_sqlConnectionFactory.GetOpenConnection());
            return locationResponse;
        }

        public async Task<List<VehiclePositionDto>> GetByDate(int VehicleId)
        {
            var locationResponse =
                await LocationDataProvider.GetVehicleLocationByDate(_sqlConnectionFactory.GetOpenConnection());
            return locationResponse;
        }
    }
}