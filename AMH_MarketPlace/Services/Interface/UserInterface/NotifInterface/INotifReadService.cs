using AMH_MarketPlace.Entities.User.SubUser.Notifications;

namespace AMH_MarketPlace.Services.Interface.UserInterface.NotifInterface
{
    public interface INotifReadService
    {
        Task<NotifRead> GetOrSaveNotifRead(bool isRead);
    }
}
