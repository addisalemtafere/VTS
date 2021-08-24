using System;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Models.Authentication;
using Domain.Entities;

namespace Infrastructure.Seed
{
    public static class SeedData
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager,
            IRepository<Vehicle> repository,
            ILocationRepository locationRepository
            )
        {
            var applicationUser = new ApplicationUser
            {
                Name = "Addisalem",
                UserName = "addis",
                Email = "addisalem12@gmail.com",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(applicationUser.Email);
            if (user == null) await userManager.CreateAsync(applicationUser, "Plural&01?");
            var checkIfExist =await repository.ListAllAsync();
            if (checkIfExist.Count == 0 && user!=null)
            {

                var vehicle = new Vehicle()
                {
                    Name = "Dezire",
                    UserId = user.Id
                };
                var trackerRequest = new TrackingDevice()
                {
                    Imei = Guid.NewGuid(),
                    Name = "GPS Tracker",
                    TrackingDeviceStatus = TrackingDeviceStatus.On
                };
                vehicle.TrackingDevice = trackerRequest;
                vehicle = await repository.AddAsync(vehicle);


                var locationRequest = new Location()
                {
                    Altitude = 0,
                    Latitude = 13.736717,
                    Longitude = 100.523186,
                    VehicleId = vehicle.Id
                };

                var location = await locationRepository.AddAsync(locationRequest);
            }
        }
    }
}