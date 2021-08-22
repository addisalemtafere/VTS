using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Contracts.Services.Identity;
using AutoMapper;
using MediatR;

namespace Application.Features.Locations.Commands
{
    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, CreateLocationCommandResponse>
    {
        private readonly IRepository<Domain.Entities.Location> _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public CreateLocationCommandHandler(IMapper mapper,
            IRepository<Domain.Entities.Location> repository,
            IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
            _repository = repository;
        }

        public async Task<CreateLocationCommandResponse> Handle(CreateLocationCommand request,
            CancellationToken cancellationToken)
        {
            var createLocationCommandResponse = new CreateLocationCommandResponse();

            var validator = new CreateLocationCommandValidator();
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
                var locationRequest = new Domain.Entities.Location()
                {
                    Altitude = request.Altitude,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    Speed = request.Speed,
                    HorizontalAccuracy = request.HorizontalAccuracy,
                    VerticalAccuracy = request.VerticalAccuracy,
                    VehicleId = request.VehicleId
                };

                var location = await _repository.AddAsync(locationRequest);
                createLocationCommandResponse.Location = _mapper.Map<CreateLocationDto>(location);
            }

            return createLocationCommandResponse;
        }
    }
}