using Domain.Entities;
using MediatR;
using System;

namespace Application.Features.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommand : IRequest<CreateVehicleCommandResponse>
    {
        //public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string BodyType { get; set; }

        //user

        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        //device

        public Guid Imei { get; set; }
        public string TrackingDeviceName { get; set; }
        public TrackingDeviceStatus Status { get; set; }
    }
}