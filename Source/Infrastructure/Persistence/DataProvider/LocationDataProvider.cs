using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using Application.Features.Locations.Queries.GetVehicleCurrentPosition;

namespace Infrastructure.Persistence.DataProvider
{
    public class LocationDataProvider
    {
        public static async Task<VehicleCurrentLocationDto> GetVehicleLocation(IDbConnection connection)
        {
            var location = await connection.QueryAsync<VehicleCurrentLocationDto>("Select * from [dbo].[locations] ");
            return (VehicleCurrentLocationDto) location;
        }

        public static async Task<List<VehiclePositionDto>> GetVehicleLocationByDate(IDbConnection connection)
        {
            var location = await connection.QueryAsync<VehiclePositionDto>("Select * from [dbo].[locations] ");
            return (List<VehiclePositionDto>) location;
        }
    }
}