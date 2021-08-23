using MediatR;
using System;

namespace Application.Features.Locations.Commands
{
    public class CreateLocationCommand : IRequest<CreateLocationCommandResponse>
    {
        public int LocationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public int VehicleId { get; set; }
        public Guid UserId { get; set; }
        public string Locality { get; set; }
    }
}