using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persisitance;
using Domain.Entities;

namespace VTS.Application.Features.Devices.Query
{
    public class GetDeviceDetailQueryHandler : IRequestHandler<GetDeviceDetailQuery, DeviceDetailVM>

    {
        private readonly IRepository<TrackingDevice> _repository;
        private readonly IMapper _mapper;

        public GetDeviceDetailQueryHandler(IRepository<TrackingDevice> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<DeviceDetailVM> Handle(GetDeviceDetailQuery request, CancellationToken cancellationToken)
        {
            var deviceDetial = await _repository.GetByIdAsync(request.Id);

            return _mapper.Map<TrackingDevice, DeviceDetailVM>(deviceDetial);
        }
    }
}