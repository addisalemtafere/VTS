using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Vehicle : AuditableEntity
    {
        public int Id { get; set; }
        public VehicleType VehicleType { get; set; }
        public string Name { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string BodyType { get; set; }
        public int TrackingDeviceId { get; set; }
        public TrackingDevice TrackingDevice { get; set; }
        public string UserId { get; set; }
    }

    public enum VehicleType
    {
        Car,
        Truck
    }
}