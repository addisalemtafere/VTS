using FluentValidation;

namespace Application.Features.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandValidator: AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 10 characters.");
        }
    }
}
