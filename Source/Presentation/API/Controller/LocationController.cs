using Application.Contracts;
using Application.Features.Locations.Commands;
using Application.Features.Locations.Queries.GetVehicleCurrentPosition;
using Application.Features.Locations.Queries.GetVehiclePositionByDateQuery;
using Application.Features.Vehicles.Commands.CreateVehicle;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILoggedInUserService _loggedInUserService;

        public LocationController(IMediator mediator,
            ILoggedInUserService loggedInUserService)
        {
            _mediator = mediator;
            _loggedInUserService = loggedInUserService;
        }

        [HttpPost("RecordVehiclePosition", Name = "RecordVehiclePosition")]
        public async Task<ActionResult<CreateVehicleCommandResponse>> Create(
            [FromBody] CreateLocationCommand createLocationCommand)
        {
            var response = await _mediator.Send(createLocationCommand);
            return Ok(response);
        }

        [HttpGet("RetrieveVehicleCurrentPosition", Name = "RetrieveVehicleCurrentPosition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GetVehicleCurrentLocationQueryResponse>> GetVehicleCurrentPosition(int VehicleId)
        {
            var getCurrentPosition = new GetVehicleCurrentPositionQuery() { VehicleId = VehicleId };
            var vehiclePosition = await _mediator.Send(getCurrentPosition);
            return Ok(vehiclePosition);
        }

        [HttpGet("RetrieveVehiclePositionByDate", Name = "RetrieveVehiclePositionByDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GetVehiclePositionByDateQueryResponse>> GetVehiclePositionByDate(int VehicleId,
            DateTime FromDate, DateTime ToDate)
        {
            var user = _loggedInUserService.UserId;
            var getPosition = new GetVehiclePositionByDateQuery()
            {
                VehicleId = VehicleId,
                FromDate = FromDate,
                ToDate = ToDate
            };
            var vehiclePosition = await _mediator.Send(getPosition);
            return Ok(vehiclePosition);
        }
    }
}