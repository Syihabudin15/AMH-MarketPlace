using AMH_MarketPlace.DTOs;
using AMH_MarketPlace.DTOs.StoreDto;
using AMH_MarketPlace.Entities.Store;
using AMH_MarketPlace.Services.Interface.StoreInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace AMH_MarketPlace.Controllers.StoreController
{
    [Route("api/store")]
    public class StoreController : BaseController
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost, Authorize(Roles = "User")]
        public async Task<IActionResult> CreateStore([FromForm] StoreRequest req)
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _storeService.CreateStore(req, user);

            CommonResponse<StoreResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Create Store Success",
                Data = result
            };
            return Created("store", response);
        }

        [HttpGet, Authorize(Roles = "User")]
        public async Task<IActionResult> GetMyStore()
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _storeService.GetMyStore(user);

            CommonResponse<StoreResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Get my Store Success",
                Data = result
            };
            return Ok(response);
        }

        [HttpPut("update-store"), Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateStore([FromForm] StoreRequest req)
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _storeService.UpdateStore(req, user);

            CommonResponse<StoreResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Update Store Success",
                Data = result
            };
            return Ok(response);
        }

        [HttpPut("update-rate"), Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateRateStore([FromQuery] int numberRate, [FromQuery] string id)
        {
            var result = await _storeService.UpdateRateStore(numberRate,id);

            CommonResponse<RateStore> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Update Rate Store Success",
                Data = result
            };
            return Ok(response);
        }
    }
}
