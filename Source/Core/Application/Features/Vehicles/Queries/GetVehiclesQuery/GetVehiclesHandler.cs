using Application.Contracts.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Queries.GetVehiclesQuery
{
    public class GetVehiclesHandler : IRequestHandler<GetVehiclesQuery,
        GetVehiclesQueryResponse>
    {
        private readonly IRepository<Vehicle> _repository;
        private readonly IMapper _mapper;

        public GetVehiclesHandler(IRepository<Vehicle> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetVehiclesQueryResponse> Handle(GetVehiclesQuery request,
            CancellationToken cancellationToken)
        {
            var vehiclesQueryResponse = new GetVehiclesQueryResponse();
            var list = await _repository.GetPagedResponseAsync(request.Page, request.Size);
            var allVehicleDto = _mapper.Map<List<VehicleDetailDto>>(list);

            vehiclesQueryResponse.Vehicles = allVehicleDto;
            vehiclesQueryResponse.Count = allVehicleDto.Count;
            vehiclesQueryResponse.Page = request.Page;
            vehiclesQueryResponse.Size = request.Size;

            return vehiclesQueryResponse;
        }
    }
}