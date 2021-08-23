using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Locations.Commands;
using Application.Features.Locations.Queries.GetVehicleCurrentPosition;
using AutoMapper;
using Domain.Entities;
using GoogleMaps.LocationServices;
using MediatR;

namespace Application.Features.Locations.Queries.GetVehiclePositionByDateQuery
{
    public class GetVehiclePositionByDateQueryHandler : IRequestHandler<GetVehiclePositionByDateQuery,
        GetVehiclePositionByDateQueryResponse>
    {
        private readonly IRepository<Location> _repository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public GetVehiclePositionByDateQueryHandler(IRepository<Location> repository,
            ILocationRepository locationRepository,
            IMapper mapper)
        {
            _repository = repository;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<GetVehiclePositionByDateQueryResponse> Handle(GetVehiclePositionByDateQuery request,
            CancellationToken cancellationToken)
        {
            var createLocationCommandResponse = new GetVehiclePositionByDateQueryResponse();

            var validator = new GetVehiclePositionByDateQueryQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Count > 0)
            {
                createLocationCommandResponse.Success = false;
                createLocationCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                    createLocationCommandResponse.ValidationErrors.Add(error.ErrorMessage);
            }

            if (createLocationCommandResponse.Success)
            {
                var locationResponse = await _locationRepository.GetByDate(request.VehicleId);

                var locationDto = _mapper.Map<List<VehiclePositionDto>>(locationResponse);

                createLocationCommandResponse.VehiclePosition = locationDto;
            }

            return createLocationCommandResponse;
        }
    }
}