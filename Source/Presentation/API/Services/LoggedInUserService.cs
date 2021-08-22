using Application.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace API.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            //UserName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            UserId = httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value;
        }

       // public string UserName { get; }
        public string UserId { get; }
    }
}