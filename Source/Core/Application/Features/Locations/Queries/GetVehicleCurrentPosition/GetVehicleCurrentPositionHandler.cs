using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Locations.Commands;
using AutoMapper;
using Domain.Entities;
using GoogleMaps.LocationServices;
using MediatR;

namespace Application.Features.Locations.Queries.GetVehicleCurrentPosition
{
    public class GetVehicleCurrentPositionHandler : IRequestHandler<GetVehicleCurrentPositionQuery,
        GetVehicleCurrentLocationQueryResponse>
    {
        private readonly IRepository<Location> _repository;
        private readonly IMapper _mapper;

        public GetVehicleCurrentPositionHandler(IRepository<Location> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
                {
                    createLocationCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (createLocationCommandResponse.Success)
            {
                var location = await _repository.GetByIdAsync(request.VehicleId);
                var gls = new GoogleLocationService();


                var locationDto = _mapper.Map<VehicleCurrentLocationDto>(location);

                try
                {
                    var adress3 = gls.GetRegionFromLatLong(locationDto.Latitude, locationDto.Longitude);
                    var adress = gls.GetAddressFromLatLang(locationDto.Latitude, locationDto.Longitude).ToString();

                }
                catch (System.Exception ex)
                {

                    throw;
                }
                createLocationCommandResponse.CurrentLocation = locationDto;
            }

            return createLocationCommandResponse;
        }
    }
}