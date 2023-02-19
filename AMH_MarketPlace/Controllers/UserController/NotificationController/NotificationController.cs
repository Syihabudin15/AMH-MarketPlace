using AMH_MarketPlace.DTOs;
using AMH_MarketPlace.DTOs.UserDto.NotificationDto;
using AMH_MarketPlace.Entities.Enum;
using AMH_MarketPlace.Services.Interface.UserInterface;
using AMH_MarketPlace.Services.Interface.UserInterface.NotifInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.WebSockets;
using System.Security.Claims;

namespace AMH_MarketPlace.Controllers.UserController.NotificationController
{
    [Route("api/notif")]
    public class NotificationController : BaseController
    {
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;

        public NotificationController(IUserService userService, INotificationService notificationService)
        {
            _userService = userService;
            _notificationService = notificationService;
        }

        [HttpPost("create"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateNotifPromo([FromForm] NotifRequest request)
        {
            var result = await _notificationService.CreateNotification(request, null, "Promo");
            CommonResponse<NotifResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Create Notification Promo Success",
                Data = result
            };
            return Ok(response);
        }

        [HttpGet("info"), Authorize(Roles = "User")]
        public async Task<IActionResult> GetAlMyNotif()
        {
            var user = User.FindFirst(ClaimTypes.Email).Value;
            var result = await _userService.GetMyNotification(user);
            CommonResponse<List<NotifResponse>> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Get all Notification Success",
                Data = result
            };
            return Ok(response);
        }

        [HttpGet("promo")]
        public async Task<IActionResult> GetAllNotificationPromo()
        {
            var result = await _notificationService.GetAllNotificationPromo();
            CommonResponse<List<NotifResponse>> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Get all Notification Promo Success",
                Data = result
            };
            return Ok(response);
        }

        [HttpPut("update-notif"), Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateStatusIsRead([FromQuery] string id)
        {
            var result = await _notificationService.UpdateIsReadStatus(Guid.Parse(id));
            CommonResponse<NotifResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Notification hasbeen read",
                Data = result
            };
            return Ok(response);
        }
    }
}
