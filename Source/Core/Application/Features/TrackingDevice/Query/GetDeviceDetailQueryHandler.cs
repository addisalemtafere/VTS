using Application.Contracts.Repositories;
using Application.Features.Devices.Query;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.TrackingDevices.Query
{
    public class
        GetTrackingDeviceDetailQueryHandler : IRequestHandler<GetTrackingDeviceDetailQuery, TrackingDeviceDetailVM>

    {
        private readonly IRepository<TrackingDevice> _repository;
        private readonly IMapper _mapper;

        public GetTrackingDeviceDetailQueryHandler(IRepository<TrackingDevice> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<TrackingDeviceDetailVM> Handle(GetTrackingDeviceDetailQuery request,
            CancellationToken cancellationToken)
        {
            var TrackingDeviceDetial = await _repository.GetByIdAsync(request.Id);

            return _mapper.Map<TrackingDevice, TrackingDeviceDetailVM>(TrackingDeviceDetial);
        }
    }
}