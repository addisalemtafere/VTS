using Application.Contracts.Repositories;
using Application.Features.Locations.Queries.GetVehicleCurrentPosition;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Services.GoogleGeocodingService;

namespace Application.Features.Locations.Queries.GetVehiclePositionByDateQuery
{
    public class GetVehiclePositionByDateQueryHandler : IRequestHandler<GetVehiclePositionByDateQuery,
        GetVehiclePositionByDateQueryResponse>
    {
        private readonly IRepository<Location> _repository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;
        private readonly IGeocodingService _geocodingService;

        public GetVehiclePositionByDateQueryHandler(IRepository<Location> repository,
            ILocationRepository locationRepository,
            IGeocodingService geocodingService,
            IMapper mapper)
        {
            _repository = repository;
            _locationRepository = locationRepository;
            _mapper = mapper;
            ;
            _geocodingService = geocodingService;
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

            if (!createLocationCommandResponse.Success) return createLocationCommandResponse;
            var locationResponse =
                await _locationRepository.GetByDate(request.VehicleId, request.FromDate, request.ToDate);

            var locationDto = _mapper.Map<List<VehiclePositionDto>>(locationResponse);

            foreach (var vehiclePositionDto in locationDto)
            {
                var localityAddress =
                    _geocodingService.GetAddressLocationAsync(vehiclePositionDto.Latitude,
                        vehiclePositionDto.Longitude);
                vehiclePositionDto.Locality = await localityAddress;
            }

            createLocationCommandResponse.VehiclePosition = locationDto;

            return createLocationCommandResponse;
        }
    }
}