using System;
using System.Threading.Tasks;
using DotnetVue.ElementAdmin.Authentication;
using DotnetVue.ElementAdmin.DataModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DotnetVue.ElementAdmin.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
        private static UserData user = new UserData();
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpGet("")]
        [Authorize("admin")]
        public UserData Index()
        {
            return user;
        }

        [HttpPost("login")]
        public LoginResponseDto Login(LoginRequestDto requestDto)
        {
            if (user.Account != requestDto.UserName || user.Password != requestDto.Password)
            {
                return new LoginResponseDto(false, "invalid account password.");
            }
            var securityKey = _configuration.GetSection("Jwt:SecurityKey").Value;
            var issuer = _configuration.GetSection("Jwt:Issuer").Value;
            var audience = _configuration.GetSection("Jwt:Audience").Value;

            var token = AccessTokenGenerator.GenerateToken(securityKey, issuer, audience, user.Id.ToString(),
                DateTime.Now.AddHours(2), user.Permissions);
            return new LoginResponseDto(true, "login success.", token);
        }

        [HttpPost("logout")]
        [Authorize("admin")]
        public async Task<ResponseDto> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new ResponseDto(true, "logout success");
        }
    }
}
