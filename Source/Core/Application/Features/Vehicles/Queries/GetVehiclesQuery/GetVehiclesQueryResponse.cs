using Application.Responses;
using System.Collections.Generic;

namespace Application.Features.Vehicles.Queries.GetVehiclesQuery
{
    public class GetVehiclesQueryResponse : BaseResponse
    {
        public GetVehiclesQueryResponse() : base()
        {
        }

        public int Count { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public List<VehicleDetailDto> Vehicles { get; set; }
    }
}