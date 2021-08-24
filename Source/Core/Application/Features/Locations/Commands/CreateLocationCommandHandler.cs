using Application.Contracts;
using Application.Contracts.Repositories;
using Application.Contracts.Services.Identity;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Locations.Commands
{
    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, CreateLocationCommandResponse>
    {
        private readonly IRepository<Location> _repository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILoggedInUserService _loggedInUserService;

        public CreateLocationCommandHandler(IMapper mapper,
            IRepository<Location> repository,
            IVehicleRepository vehicleRepository,
            ILoggedInUserService loggedInUserService,
            IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
            _repository = repository;
            _vehicleRepository = vehicleRepository;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<CreateLocationCommandResponse> Handle(CreateLocationCommand request,
            CancellationToken cancellationToken)
        {
            var createLocationCommandResponse = new CreateLocationCommandResponse();

            var validator = new CreateLocationCommandValidator(_vehicleRepository, _loggedInUserService);
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
                var locationRequest = new Location()
                {
                    Altitude = request.Altitude,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    VehicleId = request.VehicleId
                };

                var location = await _repository.AddAsync(locationRequest);
                createLocationCommandResponse.Location = _mapper.Map<CreateLocationDto>(location);
            }

            return createLocationCommandResponse;
        }
    }
}