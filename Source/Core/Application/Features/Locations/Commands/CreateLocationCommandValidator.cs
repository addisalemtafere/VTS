using Application.Contracts;
using Application.Contracts.Repositories;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Locations.Commands
{
    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILoggedInUserService _loggedInUserService;

        public CreateLocationCommandValidator(IVehicleRepository vehicleRepository,
            ILoggedInUserService loggedInUserService)

        {
            _vehicleRepository = vehicleRepository;
            _loggedInUserService = loggedInUserService;

            RuleFor(e => e)
                .MustAsync(VehicleIsExist)
                .WithMessage("An Vehicle with the  identity not found");

            RuleFor(e => e)
                .MustAsync(CheckUserCanAddOrUpdateLocation)
                .WithMessage("User has no privilege to update or add vehicle position");



            RuleFor(p => p.Altitude)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(p => p.Longitude)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(p => p.Latitude)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(p => p.VehicleId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} is required");
        }

        private async Task<bool> VehicleIsExist(CreateLocationCommand request, CancellationToken token)
        {
            var vehicle = _vehicleRepository.GetByIdAsync(request.VehicleId);
            var isVehicleExist = await vehicle != null;
            return isVehicleExist;
        }

        private async Task<bool> CheckUserCanAddOrUpdateLocation(CreateLocationCommand request, CancellationToken token)
        {
            var isValidUser = request.UserId.ToString() == _loggedInUserService.UserId;
            return isValidUser;
        }
    }
}