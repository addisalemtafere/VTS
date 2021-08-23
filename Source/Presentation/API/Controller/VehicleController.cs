using Application.Features.Vehicles.Commands.CreateVehicle;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Features.Vehicles.Queries.GetVehiclesQuery;

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

        [HttpPost("RegisterVehicle", Name = "RegisterVehicle")]
        public async Task<ActionResult<CreateVehicleCommandResponse>> Create(
            [FromBody] CreateVehicleCommand createVehicleCommand)
        {
            var response = await _mediator.Send(createVehicleCommand);
            return Ok(response);
        }


        [HttpGet]
        [Route("GetAllVehiclesWithDevices", Name = "GetAllVehiclesWithDevices")]
        public async Task<ActionResult<GetVehiclesQueryResponse>> GetAllVehiclesWithDevices(int page, int size)
        {
            var getVehiclesQuery = new GetVehiclesQuery() {Page = page, Size = size};
            var vehicles = await _mediator.Send(getVehiclesQuery);
            return Ok(vehicles);
        }
    }
}

