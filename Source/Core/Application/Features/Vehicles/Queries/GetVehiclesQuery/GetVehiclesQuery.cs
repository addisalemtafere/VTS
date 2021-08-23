using MediatR;

namespace Application.Features.Vehicles.Queries.GetVehiclesQuery
{
    public class GetVehiclesQuery : IRequest<GetVehiclesQueryResponse>
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}