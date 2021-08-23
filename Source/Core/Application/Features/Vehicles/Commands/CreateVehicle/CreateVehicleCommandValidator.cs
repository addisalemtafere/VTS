using Application.Contracts.Repositories;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        private readonly ITrackingDeviceRepository _trackingDevicerepository;

        public CreateVehicleCommandValidator(ITrackingDeviceRepository trackingDevicerepository)
        {
            _trackingDevicerepository = trackingDevicerepository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(e => e)
                .MustAsync(VehicleTrackerDeviceUnique)
                .WithMessage("A Vehicle with the same identity already exists.");
        }

        private async Task<bool> VehicleTrackerDeviceUnique(CreateVehicleCommand request, CancellationToken token)
        {
            var device = await _trackingDevicerepository.GetByImeiAsync(request.Imei);
            return device == null;
        }
    }
}