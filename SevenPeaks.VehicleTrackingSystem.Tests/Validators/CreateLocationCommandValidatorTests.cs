using Application.Contracts;
using Application.Contracts.Repositories;
using Application.Features.Locations.Commands;
using Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace SevenPeaks.VehicleTrackingSystem.Tests.Validators
{
    public class CreateLocationCommandValidatorTests
    {
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
        private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
        private readonly CreateLocationCommandValidator _createLocationCommandValidator;
        public CreateLocationCommandValidatorTests()
        {
            _vehicleRepositoryMock = new Mock<IVehicleRepository>();
            _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            _createLocationCommandValidator = new CreateLocationCommandValidator(_vehicleRepositoryMock.Object, _loggedInUserServiceMock.Object);
        }

        [Fact]
        public void VehicleShouldExistWithIdentity()
        {
            _vehicleRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Vehicle()
                {
                    Id = 1,
                    Name = "vehicle-one"
                });

            var command = new CreateLocationCommand()
            {
                VehicleId = 2,
                LocationId = 1
            };

            var result =  _createLocationCommandValidator.Validate(command);
            _vehicleRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            Assert.NotNull(result);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void CreateLocationCommandShouldBeValid()
        {
            _vehicleRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Vehicle()
                {
                    Id = 1,
                    Name = "vehicle-one"
                });
          
            var userId = Guid.NewGuid();
            
            _loggedInUserServiceMock.Setup(x => x.UserId).Returns(userId.ToString());
            var command = new CreateLocationCommand()
            {
                VehicleId = 2,
                LocationId = 1,
                UserId = userId,
                Altitude = 29.0,
                Longitude = 60.56,
                Latitude = 76.78,
            };

            var result = _createLocationCommandValidator.Validate(command);
            _vehicleRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
            Assert.NotNull(result);
            result.IsValid.Should().BeTrue();
        }
    }
}
