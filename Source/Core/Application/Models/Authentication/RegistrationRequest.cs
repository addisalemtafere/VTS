using System.ComponentModel.DataAnnotations;

namespace Application.Models.Authentication
{
    public class RegistrationRequest
    {
        [Required] public string Name { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        [Required] [MinLength(6)] public string UserName { get; set; }

        [Required] [MinLength(6)] public string Password { get; set; }
    }
}