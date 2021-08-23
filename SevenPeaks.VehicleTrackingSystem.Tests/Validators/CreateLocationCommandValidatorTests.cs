using Application.Contracts;
using Application.Contracts.Repositories;
using Application.Features.Locations.Commands;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
