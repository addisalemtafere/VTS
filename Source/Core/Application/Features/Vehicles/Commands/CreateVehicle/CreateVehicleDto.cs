namespace Application.Features.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleDto
    {
        public int VehicleId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}