using System;

namespace Application.Features.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
    }
}
