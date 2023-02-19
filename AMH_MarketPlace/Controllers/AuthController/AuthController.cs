using AMH_MarketPlace.DTOs;
using AMH_MarketPlace.DTOs.AuthDto;
using AMH_MarketPlace.Services.Interface.AuthInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AMH_MarketPlace.Controllers.AuthController
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register"), AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterRequest request)
        {
            var result = await _authService.RegisterUser(request);
            CommonResponse<RegisterResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Register Success",
                Data = result
            };
            return Created("register-user", response);
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            var result = await _authService.Login(request);
            CommonResponse<LoginResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Login Success",
                Data = result
            };
            return Ok(response);
        }
    }
}
