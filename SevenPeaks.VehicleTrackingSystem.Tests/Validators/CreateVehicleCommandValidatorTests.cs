using Application.Contracts.Repositories;
using Application.Features.Vehicles.Commands.CreateVehicle;
using FluentAssertions;
using Moq;
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
            var command = new CreateVehicleCommand() 
            {
            Name = "vehicle-one",
            };
            var result = _createVehicleCommandValidator.Validate(command);
            result.IsValid.Should().BeTrue();
        }
    }
}
