using Application.Contracts.Repositories;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Queries.GetVehiclesQuery
{
    public class GetVehiclesHandler : IRequestHandler<GetVehiclesQuery,
        GetVehiclesQueryResponse>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public GetVehiclesHandler(IVehicleRepository vehicleRepository,
            IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<GetVehiclesQueryResponse> Handle(GetVehiclesQuery request,
            CancellationToken cancellationToken)
        {
            var vehiclesQueryResponse = new GetVehiclesQueryResponse();
            var list = await _vehicleRepository.GetPagedVehicle(request.Page, request.Size);
            var countVehicles = await _vehicleRepository.countVehicle();
            var allVehicleDto = _mapper.Map<List<VehicleDetailDto>>(list);

            vehiclesQueryResponse.Vehicles = allVehicleDto;
            vehiclesQueryResponse.Count = countVehicles;
            vehiclesQueryResponse.Page = request.Page;
            vehiclesQueryResponse.Size = request.Size;

            return vehiclesQueryResponse;
        }
    }
}