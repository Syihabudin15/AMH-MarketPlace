using AMH_MarketPlace.DTOs;
using AMH_MarketPlace.DTOs.BankDto.WalletDto;
using AMH_MarketPlace.Entities.Bank.Wallet;
using AMH_MarketPlace.Services.Interface.WalletInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace AMH_MarketPlace.Controllers.WalletController
{
    [Route("api/wallet")]
    public class WalletController : BaseController
    {
        private readonly IUserWalletService _userWalletService;

        public WalletController(IUserWalletService userWalletService)
        {
            _userWalletService = userWalletService;
        }

        [HttpPost, Authorize(Roles = "User")]
        public async Task<IActionResult> CreateWallet([FromForm] WalletRequest req)
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _userWalletService.CreateUserWallet(req, user);
            CommonResponse<WalletResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Create Wallet Success",
                Data = result
            };
            return Created("wallet", response);
        }

        [HttpGet("me"), Authorize(Roles = "User")]
        public async Task<IActionResult> GetMyWallet()
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _userWalletService.GetMyWallet(user);
            CommonResponse<WalletResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Get my Walet Success",
                Data = result
            };
            return Ok(response);
        }

        [HttpPut("update-wallet"), Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateMyWallet([FromForm] WalletRequest req)
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _userWalletService.UpdateMyWallet(req, user);
            CommonResponse<WalletResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Update Wallet Success",
                Data = result
            };
            return Ok(response);
        }

        [HttpPut("update-verify"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatusVerify([FromQuery] string id)
        {
            var result = await _userWalletService.VerifyUserWallet(id);
            CommonResponse<WalletResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Update Status Verify Success",
                Data = result
            };
            return Ok(response);
        }
    }
}
