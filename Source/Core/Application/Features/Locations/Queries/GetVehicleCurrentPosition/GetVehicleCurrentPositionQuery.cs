using FluentValidation;
using MediatR;

namespace Application.Features.Locations.Queries.GetVehicleCurrentPosition
{
    public class GetVehicleCurrentPositionQuery : IRequest<GetVehicleCurrentLocationQueryResponse>
    {
        public int VehicleId { get; set; }
    }

    public class GetVehicleCurrentPositionQueryValidator : AbstractValidator<GetVehicleCurrentPositionQuery>
    {
        public GetVehicleCurrentPositionQueryValidator()
        {
            RuleFor(p => p.VehicleId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}