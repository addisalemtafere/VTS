using Application.Features.Vehicles.Commands.CreateVehicle;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Locations.Commands;
using Application.Features.Locations.Queries.GetVehicleCurrentPosition;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
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
            var getCurrentPosition = new GetVehicleCurrentPositionQuery() {VehicleId = VehicleId};
            var vehiclePosition = await _mediator.Send(getCurrentPosition);
            return Ok(vehiclePosition);
        }
    }
}