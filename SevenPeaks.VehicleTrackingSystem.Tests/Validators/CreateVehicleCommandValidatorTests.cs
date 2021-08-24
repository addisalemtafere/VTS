using Application.Contracts.Repositories;
using Application.Features.Vehicles.Commands.CreateVehicle;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace SevenPeaks.VehicleTrackingSystem.Tests.Validators
{
    public class CreateVehicleCommandValidatorTests
    {
        private readonly Mock<ITrackingDeviceRepository> _trackingDevicerepositoryMock;
        private readonly CreateVehicleCommandValidator _createVehicleCommandValidator;
        public CreateVehicleCommandValidatorTests()
        {
            _trackingDevicerepositoryMock = new Mock<ITrackingDeviceRepository>();
            _createVehicleCommandValidator = new CreateVehicleCommandValidator(_trackingDevicerepositoryMock.Object);
        }

        [Fact]
        public void RequestCommandObjectShouldBeInvalid()
        {
            var command = new CreateVehicleCommand() { };
            var result = _createVehicleCommandValidator.Validate(command);
            result.IsValid.Should().BeFalse();
        }
        [Fact]
        public void RequestCommandObjectShouldBeValid()
        {
            var imeiNumber = Guid.NewGuid();
            var command = new CreateVehicleCommand() 
            {
            Name = "vehicle-one",
            ImeiNumber = imeiNumber,
            };
            _trackingDevicerepositoryMock.Setup(x => x.GetByImeiAsync(imeiNumber)).ReturnsAsync(new Domain.Entities.TrackingDevice());
            var result = _createVehicleCommandValidator.Validate(command);
            _trackingDevicerepositoryMock.Verify(x => x.GetByImeiAsync(imeiNumber), Times.Once);
            result.IsValid.Should().BeTrue();
        }
    }
}
