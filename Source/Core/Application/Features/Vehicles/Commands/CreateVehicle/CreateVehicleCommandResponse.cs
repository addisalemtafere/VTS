
using Application.Responses;

namespace Application.Features.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandResponse: BaseResponse
    {
        public CreateVehicleCommandResponse(): base()
        {

        }

        public CreateVehicleDto Vehicle { get; set; }
    }
}