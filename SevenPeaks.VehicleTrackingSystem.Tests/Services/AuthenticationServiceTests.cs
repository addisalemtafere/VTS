using Application.Contracts.Services.Identity;
using Application.Models.Authentication;
using Infrastructure.Models;
using Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Threading.Tasks;
using Xunit;

namespace SevenPeaks.VehicleTrackingSystem.Tests.Services
{
    public class AuthenticationServiceTests
    {
        //private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        //private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        //private readonly Mock<IOptions<JwtSettings>> _jwtSettingsMock;

        //private readonly IAuthenticationService _authenticationService;
        //public AuthenticationServiceTests()
        //{
        //    _userManagerMock = new Mock<UserManager<ApplicationUser>>();
        //    _signInManagerMock = new Mock<SignInManager<ApplicationUser>>();
        //    _jwtSettingsMock = new Mock<IOptions<JwtSettings>>();
        //    _authenticationService = new AuthenticationService(_userManagerMock.Object, _jwtSettingsMock.Object, _signInManagerMock.Object);
        //}
        //[Fact]
        //public async Task AuthenticationShouldPassWhenValidUsernameAndCredentials()
        //{
        //    var request = new AuthenticationRequest()
        //    {
        //        Email = "addis@gmail.com",
        //        Password = "P@ssw0rd"
        //    };
        //    _userManagerMock.Setup(x => x.FindByEmailAsync(request.Email)).ReturnsAsync(new ApplicationUser()
        //    {
        //       Email = "addis@gmail.com",
        //       AccessFailedCount = 0,
        //       EmailConfirmed = true,
        //       FirstName = "Addisalem",
        //       LastName = "Addis",
        //       PhoneNumber= "0945765686",
        //       Id= "1",
        //       UserName = "addis@gmail.com",
        //       TwoFactorEnabled = true
        //    });

        //    var signInResult = new SignInResult();
        //     _signInManagerMock.Protected().SetupGet<bool>("Succeeded").Returns(true);

         
        //}
    }
}
