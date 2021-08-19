using Application.Features.TrackingDevices;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Devices.Query
{
    public class GetTrackingDeviceListQery : IRequest<List<TrackingDeviceVM>>
    {
    }
}