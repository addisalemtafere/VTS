using FluentValidation;
using MediatR;
using System;

namespace Application.Features.Locations.Queries.GetVehiclePositionByDateQuery
{
    public class GetVehiclePositionByDateQuery : IRequest<GetVehiclePositionByDateQueryResponse>
    {
        public int VehicleId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class GetVehiclePositionByDateQueryQueryValidator : AbstractValidator<GetVehiclePositionByDateQuery>
    {
        public GetVehiclePositionByDateQueryQueryValidator()
        {
            RuleFor(p => p.VehicleId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.FromDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .WithMessage("FromDate null is not allowed");

            RuleFor(p => p.ToDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .WithMessage("ToDate parameter is not allowed");
        }
    }
}