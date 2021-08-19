using MediatR;

namespace VTS.Application.Features.Devices.Query
{
    public class GetDeviceDetailQuery : IRequest<DeviceDetailVM>
    {
        public int Id { get; set; }
    }
}