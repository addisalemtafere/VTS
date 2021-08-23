using API.Controller;
using Application.Contracts;
using Application.Features.Locations.Commands;
using Application.Features.Locations.Queries.GetVehicleCurrentPosition;
using Application.Features.Locations.Queries.GetVehiclePositionByDateQuery;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SevenPeaks.VehicleTrackingSystem.Tests.API
{
    public class LocationControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;

        private readonly LocationController _locationController;
        public LocationControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            _locationController = new LocationController(_mediatorMock.Object, _loggedInUserServiceMock.Object);
        }

        [Fact]
        public async Task ShouldCreateLocation()
        {

            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateLocationCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(
                new CreateLocationCommandResponse()
                {
                  Location = new CreateLocationDto() { Id = 1}
                });

            var actionResult = await _locationController.Create(new CreateLocationCommand()
            {
                LocationId =  1,
            });

            var result = actionResult.Result as OkObjectResult;
            _mediatorMock.Verify(x => x.Send(It.IsAny<CreateLocationCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.NotNull(result);
            
        }

        [Fact]
        public async Task ShouldGetVehicleCurrentLocation()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetVehicleCurrentPositionQuery>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(
                new GetVehicleCurrentLocationQueryResponse()
                {
                    CurrentLocation = new VehicleCurrentLocationDto()
                    {
                        Altitude = 60.56,
                        Latitude = 90.30,
                        VehicleId = 1,
                    },
                    Success = true,
                }
            );

            var actionResult = await _locationController.GetVehicleCurrentPosition(1);
            var result = actionResult.Result as OkObjectResult;
            _mediatorMock.Verify(x => x.Send(It.IsAny<GetVehicleCurrentPositionQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.NotNull(result);
            var actualResult = result.Value as GetVehicleCurrentLocationQueryResponse;
            Assert.NotNull(actualResult);
            actualResult.CurrentLocation.Altitude.Should().Be(60.56);
            actualResult.CurrentLocation.Latitude.Should().Be(90.30);
        }


        [Fact]
        public async Task ShouldGetVehicleCurrentLocationByDate()
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetVehiclePositionByDateQuery>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(
                new GetVehiclePositionByDateQueryResponse()
                {
                    VehiclePosition = new System.Collections.Generic.List<VehiclePositionDto>()
                    {
                        new VehiclePositionDto()
                        {
                         Altitude = 60.56,
                        Latitude = 90.30,
                        }
                    },
                    Success = true,
                }
            );


            var actionResult = await _locationController.GetVehiclePositionByDate(1, DateTime.Now, DateTime.Now);
            var result = actionResult.Result as OkObjectResult;
            _mediatorMock.Verify(x => x.Send(It.IsAny<GetVehiclePositionByDateQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.NotNull(result);
            var actualResult = result.Value as GetVehiclePositionByDateQueryResponse;
            Assert.NotNull(actualResult);
            actualResult.VehiclePosition[0].Altitude.Should().Be(60.56);
            actualResult.VehiclePosition[0].Latitude.Should().Be(90.30);
        }
    }
}
