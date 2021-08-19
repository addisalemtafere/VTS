using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persisitance;
using Domain.Entities;

namespace VTS.Application.Features.TrackingDevices.Query
{
    public class GetTrackingDeviceListQueryHandler : IRequestHandler<GetTrackingDeviceListQery, List<TrackingDeviceVM>>

    {
        private readonly IRepository<TrackingDevice> _repository;
        private readonly IMapper _mapper;

        public GetTrackingDeviceListQueryHandler(IMapper mapper, IRepository<TrackingDevice> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<TrackingDeviceVM>> Handle(GetTrackingDeviceListQery request, CancellationToken cancellationToken)
        {
            var allTrackingDevice = await _repository.ListAllAsync();
            return _mapper.Map<List<TrackingDeviceVM>>(allTrackingDevice);
        }
    }
}