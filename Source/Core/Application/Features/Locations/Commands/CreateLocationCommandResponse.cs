using Application.Responses;

namespace Application.Features.Locations.Commands
{
    public class CreateLocationCommandResponse : BaseResponse
    {
        public CreateLocationCommandResponse() : base()
        {
        }

        public CreateLocationDto Location { get; set; }
    }
}