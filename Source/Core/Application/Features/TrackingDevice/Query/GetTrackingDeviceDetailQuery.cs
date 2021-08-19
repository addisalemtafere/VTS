using MediatR;

namespace Application.Features.Devices.Query
{
    public class GetTrackingDeviceDetailQuery : IRequest<TrackingDeviceDetailVM>
    {
        public int Id { get; set; }
    }
}