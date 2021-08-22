using Application.Responses;

namespace Application.Features.Location.Commands
{
    public class CreateLocationCommandResponse : BaseResponse
    {
        public CreateLocationCommandResponse() : base()
        {
        }

        public CreateLocationDto Location { get; set; }
    }
}