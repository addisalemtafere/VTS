using System;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Models.Authentication;
using Domain.Entities;

namespace Infrastructure.Seed
{
    public static class UserCreator
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, IRepository<Vehicle> repository)
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
            var checkIfExist = repository.ListAllAsync();
            if (checkIfExist.Result.Count == 0)
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
            }
        }
    }
}