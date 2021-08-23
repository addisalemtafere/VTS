using System;
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
            const string sql =
                "Select top 1 * from [dbo].[locations] where vehicleId = @VehicleId ORDER BY CreatedDate  DESC";

            var location = await connection.QuerySingleAsync<VehicleCurrentLocationDto>(sql, parameters);
            return location;
        }

        public static async Task<List<VehiclePositionDto>> GetVehicleLocationByDate(IDbConnection connection,
            int VehicleId, DateTime FromDate, DateTime ToDate)
        {
            var dictionary = new Dictionary<string, object>
            {
                {"@VehicleId", VehicleId},
                {"@FromDate", FromDate},
                {"@ToDate", ToDate},
            };
            var parameters = new DynamicParameters(dictionary);
            const string sql =
                "Select   * from [dbo].[locations] where VehicleId = @VehicleId and CreatedDate between @FromDate and @ToDate ";

            var location = await connection.QueryAsync<VehiclePositionDto>(sql, parameters);
            return (List<VehiclePositionDto>) location;
        }
    }
}