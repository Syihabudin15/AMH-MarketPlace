using AMH_MarketPlace.DTOs.UserDto.NotificationDto;
using AMH_MarketPlace.Entities.User.SubUser.Notifications;

namespace AMH_MarketPlace.Services.Interface.UserInterface.NotifInterface
{
    public interface INotificationService
    {
        Task<NotifResponse> CreateNotification(NotifRequest request, string? userId, string categoryNotifName);
        Task<List<NotifResponse>> GetAllMyNotification(Guid userId);
        Task<List<NotifResponse>> GetAllNotificationPromo();
        Task<NotifResponse> UpdateIsReadStatus(Guid notifId);

    }
}
