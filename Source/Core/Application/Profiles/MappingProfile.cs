using Application.Features.TrackingDevices;
using AutoMapper;
using Domain.Entities;

namespace  Application.Profiles
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TrackingDevice, TrackingDeviceVM>().ReverseMap();
        }
    }
}