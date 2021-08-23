using Application.Contracts.Repositories;
using Application.Contracts.Services.Identity;
using Application.Models.Authentication;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, CreateVehicleCommandResponse>
    {
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly ITrackingDeviceRepository _trackingDeviceRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public CreateVehicleCommandHandler(IMapper mapper,
            IRepository<Vehicle> vehicleRepository,
            ITrackingDeviceRepository trackingDeviceRepository,
            IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _authenticationService = authenticationService;
            _trackingDeviceRepository = trackingDeviceRepository;
        }

        public async Task<CreateVehicleCommandResponse> Handle(CreateVehicleCommand request,
            CancellationToken cancellationToken)
        {
            var createVehicleCommandResponse = new CreateVehicleCommandResponse();

            var validator = new CreateVehicleCommandValidator(_trackingDeviceRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Count > 0)
            {
                createVehicleCommandResponse.Success = false;
                createVehicleCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                    createVehicleCommandResponse.ValidationErrors.Add(error.ErrorMessage);
            }

            if (createVehicleCommandResponse.Success)
            {
                var userRequest = new RegistrationRequest()
                {
                    Name = request.Name,
                    Email = request.Email,
                    UserName = request.UserName,
                    Password = request.Password
                };
                var user = await _authenticationService.RegisterAsync(userRequest);
                var vehicle = new Vehicle()
                {
                    Name = request.VehicleName,
                    UserId = user.UserId
                };
                var trackerRequest = new TrackingDevice()
                {
                    Imei = request.Imei,
                    Name = request.TrackingDeviceName,
                    TrackingDeviceStatus = request.Status
                };
                vehicle.TrackingDevice = trackerRequest;
                vehicle = await _vehicleRepository.AddAsync(vehicle);

                CreateVehicleDto vehicleRespose = new CreateVehicleDto
                {
                    Name = request.Name,
                    UserId = user.UserId,
                    VehicleId = vehicle.Id
                };

                createVehicleCommandResponse.Vehicle = vehicleRespose;
            }

            return createVehicleCommandResponse;
        }
    }
}