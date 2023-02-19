using AMH_MarketPlace.Entities.User.SubUser.Notifications;

namespace AMH_MarketPlace.Services.Interface.UserInterface.NotifInterface
{
    public interface ICategoryNotifService
    {
        Task<CategoryNotification> GetOrSaveCategoryNotif(string notif);
    }
}
