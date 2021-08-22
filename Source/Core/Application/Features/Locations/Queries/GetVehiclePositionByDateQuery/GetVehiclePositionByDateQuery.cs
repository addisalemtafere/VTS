using System;
using FluentValidation;
using MediatR;

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
        }
    }
}