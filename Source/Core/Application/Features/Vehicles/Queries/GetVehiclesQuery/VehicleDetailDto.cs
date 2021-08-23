using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.TrackingDevices;

namespace Application.Features.Vehicles.Queries.GetVehiclesQuery
{
    public class VehicleDetailDto
    {
        public int Id { get; set; }
        public VehicleType VehicleType { get; set; }
        public string Name { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string BodyType { get; set; }
        public int TrackingDeviceId { get; set; }
        public TrackingDeviceVM TrackingDevice { get; set; }
        public string UserId { get; set; }
    }

    public enum VehicleType
    {
        Car,
        Truck
    }
}