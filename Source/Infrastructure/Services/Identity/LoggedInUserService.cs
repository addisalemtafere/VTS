using Application.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Services.Identity
{
    public class LoggedInUserService : ILoggedInUserService
    {
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value;
            UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }
        public string UserName { get; }
    }
}