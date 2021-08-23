using API.Controller;
using Application.Features.Vehicles.Commands.CreateVehicle;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SevenPeaks.VehicleTrackingSystem.Tests.API
{
    public class RegisterVehicleTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly VehicleController _vehicleController;
        public RegisterVehicleTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _vehicleController = new VehicleController(_mediatorMock.Object);
        }
        
        [Fact]
        public async Task ShouldRegisterVehicle()
        {
            var response = new CreateVehicleCommandResponse()
            {
                Success =  true,
                Vehicle =  new CreateVehicleDto()
                {
                    Name = "vehicle-one",
                    VehicleId = 1
                }
            };

            _mediatorMock.Setup( x => x.Send(It.IsAny<CreateVehicleCommand>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(response);

            var actionRresult = await _vehicleController.Create(new CreateVehicleCommand()
            {
                Name = "vehicle-one",
                DeviceId = 1,
            }) ;

            var result = actionRresult.Result as OkObjectResult;
            _mediatorMock.Verify(x => x.Send(It.IsAny<CreateVehicleCommand>(),
              It.IsAny<CancellationToken>()),Times.Once);
            result.Should().NotBeNull();

            var actualResponse = result.Value as CreateVehicleCommandResponse;
            actualResponse.Success.Should().BeTrue();
            actualResponse.Vehicle.Name.Should().Equals("vehicle-one");
        }
    }
}
