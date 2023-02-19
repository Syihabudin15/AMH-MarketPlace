using AMH_MarketPlace.DTOs;
using AMH_MarketPlace.DTOs.UserDto;
using AMH_MarketPlace.Services.Interface.UserInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace AMH_MarketPlace.Controllers.UserController
{
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me"), Authorize(Roles = "User")]
        public async Task<IActionResult> GetMySelf()
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _userService.GetMyUser(user);
            CommonResponse<UserResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Get my Data Success",
                Data = result
            };
            return Ok(response);
        }

        [HttpPut("update"), Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateMyAddress([FromForm] AddressRequest request)
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _userService.UpdateAddress(request, user);
            CommonResponse<UserResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Update success",
                Data = result
            };
            return Ok(response);
        }
    }
}
