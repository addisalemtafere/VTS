using Application.Features.TrackingDevices;
using Application.Features.Vehicles.Commands.CreateVehicle;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TrackingDevice, TrackingDeviceVM>().ReverseMap();
            CreateMap<Vehicle, CreateVehicleDto>();
        }
    }
}