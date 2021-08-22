using Domain.Common;
using System;

namespace Domain.Entities
{
    public class TrackingDevice : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public Guid Imei { get; set; }
        public TrackingDeviceStatus TrackingDeviceStatus { get; set; }
    }

    public enum TrackingDeviceStatus
    {
        On,
        Off,
        Bussy
    }
}