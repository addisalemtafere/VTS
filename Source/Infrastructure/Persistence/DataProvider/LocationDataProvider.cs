using Application.Features.Locations.Queries.GetVehicleCurrentPosition;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.DataProvider
{
    public class LocationDataProvider
    {
        public static async Task<VehicleCurrentLocationDto> GetVehicleLocation(IDbConnection connection, int vehicleId)
        {
            var dictionary = new Dictionary<string, object>
            {
                {"@VehicleId", vehicleId}
            };
            var parameters = new DynamicParameters(dictionary);
            var sql = "Select top 1 * from [dbo].[locations] where vehicleId = @VehicleId ORDER BY CreatedDate  DESC";

            var location = await connection.QuerySingleAsync<VehicleCurrentLocationDto>(sql, parameters);
            return location;
        }

        public static async Task<List<VehiclePositionDto>> GetVehicleLocationByDate(IDbConnection connection)
        {
            var location = await connection.QueryAsync<VehiclePositionDto>("Select * from [dbo].[locations] ");
            return (List<VehiclePositionDto>)location;
        }
    }
}