using Domain.Common;

namespace Domain.Entities
{
    public class TrackingDevice : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Model { get; set; }

        public string Phone { get; set; }

        public string OperatingSystem { get; set; }

        public string Version { get; set; }

        public string VehicleId { get; set; }
        public TrackingDeviceStatus TrackingDeviceStatus { get; set; }

        //public virtual Vehicle Vehicle { get; set; }
    }

    public enum TrackingDeviceStatus
    {
        On,
        Off,
        Bussy
    }
}