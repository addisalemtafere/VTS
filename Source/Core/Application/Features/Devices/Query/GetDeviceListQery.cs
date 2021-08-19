using MediatR;
using System.Collections.Generic;

namespace VTS.Application.Features.Devices.Query
{
    public class GetDeviceListQery : IRequest<List<DeviceVM>>
    {
    }
}