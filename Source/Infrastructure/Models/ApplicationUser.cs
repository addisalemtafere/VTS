using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
    }
}