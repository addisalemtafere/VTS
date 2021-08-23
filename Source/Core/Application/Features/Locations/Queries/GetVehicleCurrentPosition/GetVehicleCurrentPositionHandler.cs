using Application.Contracts.Repositories;
using Application.Contracts.Services.GoogleGeocodingService;
using AutoMapper;
using Domain.Entities;
using GoogleMaps.LocationServices;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Locations.Queries.GetVehicleCurrentPosition
{
    public class GetVehicleCurrentPositionHandler : IRequestHandler<GetVehicleCurrentPositionQuery,
        GetVehicleCurrentLocationQueryResponse>
    {
        private readonly IRepository<Location> _repository;
        private readonly ILocationRepository _locationRepository;
        private readonly IGeocodingService _geocodingService;
        private readonly IMapper _mapper;

        public GetVehicleCurrentPositionHandler(IRepository<Location> repository,
            IGeocodingService geocodingService,
            ILocationRepository locationRepository,
            IMapper mapper)
        {
            _repository = repository;
            _locationRepository = locationRepository;
            _mapper = mapper;
            _geocodingService = geocodingService;
        }

        public async Task<GetVehicleCurrentLocationQueryResponse> Handle(GetVehicleCurrentPositionQuery request,
            CancellationToken cancellationToken)
        {
            var createLocationCommandResponse = new GetVehicleCurrentLocationQueryResponse();

            var validator = new GetVehicleCurrentPositionQueryValidator();
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
                var location = await _locationRepository.GetCurrentPositonVehicle(request.VehicleId);
                var locationDto = _mapper.Map<VehicleCurrentLocationDto>(location);
                var localityAddress = _geocodingService.GetAddressLocationAsync(locationDto.Latitude, locationDto.Longitude);
                locationDto.Locality = await localityAddress;
                createLocationCommandResponse.CurrentLocation = locationDto;
            }

            return createLocationCommandResponse;
        }
    }
}