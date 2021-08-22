using Application.Responses;

namespace Application.Features.Locations.Queries.GetVehicleCurrentPosition
{
    public class GetVehicleCurrentLocationQueryResponse : BaseResponse
    {
        public GetVehicleCurrentLocationQueryResponse() : base()
        {
        }

        public VehicleCurrentLocationDto CurrentLocation { get; set; }
    }
}