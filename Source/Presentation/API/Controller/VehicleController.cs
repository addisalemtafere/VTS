using Application.Features.Vehicles.Commands.CreateVehicle;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehicleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("registerVehicle", Name = "RegisterVehicle")]
        public async Task<ActionResult<CreateVehicleCommandResponse>> Create(
            [FromBody] CreateVehicleCommand createVehicleCommand)
        {
            var response = await _mediator.Send(createVehicleCommand);
            return Ok(response);
        }
    }
}