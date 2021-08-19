using System;

namespace Application.Features.TrackingDevices
{
    public class TrackingDeviceVM
    {
        public int TrackingDeviceId { get; set; }
        public Guid ImeiNumber { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}