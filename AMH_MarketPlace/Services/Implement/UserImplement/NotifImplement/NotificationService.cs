using AMH_MarketPlace.DTOs.UserDto.NotificationDto;
using AMH_MarketPlace.Entities.User.SubUser.Notifications;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.UserInterface.NotifInterface;
using AMH_MarketPlace.CustomExceptions.ExceptionsHandlers;

namespace AMH_MarketPlace.Services.Implement.UserImplement.NotifImplement
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification> _repository;
        private readonly IDbPersistence _dbPersistence;
        private readonly INotifReadService _notifReadService;
        private readonly ICategoryNotifService _categoryNotifService;
        public NotificationService(IRepository<Notification> repository, IDbPersistence dbPersistence, INotifReadService notifReadService, ICategoryNotifService categoryNotifService)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
            _notifReadService = notifReadService;
            _categoryNotifService = categoryNotifService;
        }

        public async Task<NotifResponse> CreateNotification(NotifRequest request, string? userId, string categoryNotifName)
        {
            try
            {
                var isRead = await _notifReadService.GetOrSaveNotifRead(false);
                var categoryNotif = await _categoryNotifService.GetOrSaveCategoryNotif(categoryNotifName);
                var saveNotif = await _repository.Save(new Notification
                {
                    Title = request.Title,
                    Body = request.Body,
                    CreatedAt = DateTime.Now,
                    UserId = Guid.Parse(userId),
                    CategoryNotificationId = categoryNotif.Id,
                    NotifReadId = isRead.Id
                });

                await _dbPersistence.SaveChangesAsync();
                return new NotifResponse
                {
                    Id = saveNotif.Id.ToString(),
                    Title = saveNotif.Title,
                    Body = saveNotif.Body,
                    CreatedAt = saveNotif.CreatedAt.ToString(),
                    isRead = isRead.IsRead,
                    Category = categoryNotif.Name
                };
            }
            catch (Exception)
            {
                throw new Exception("Error while Create Notification");
            }
        }

        public async Task<List<NotifResponse>> GetAllMyNotification(Guid userId)
        {
            try
            {
                var notifs = await _repository.FindAll(n => n.UserId.Equals(userId), new[] { "CategoryNotification", "NotifRead" });
                var listNotif = notifs.Select(n => new NotifResponse
                {
                    Id = n.Id.ToString(),
                    Title = n.Title,
                    Body = n.Body,
                    CreatedAt = n.CreatedAt.ToString(),
                    isRead = n.NotifRead.IsRead,
                    Category = n.CategoryNotification.Name
                }).ToList();
                return listNotif;
            }
            catch (Exception)
            {
                throw new Exception("Error while get All My Notification Info");
            }
        }

        public async Task<List<NotifResponse>> GetAllNotificationPromo()
        {
            try
            {
                var notifs = await _repository.FindAll(n => n.UserId.Equals(null), new[] { "CategoryNotification", "NotifRead" });
                var listNotif = notifs.Select(n => new NotifResponse
                {
                    Id = n.Id.ToString(),
                    Title = n.Title,
                    Body = n.Body,
                    CreatedAt = n.CreatedAt.ToString(),
                    isRead = n.NotifRead.IsRead,
                    Category = n.CategoryNotification.Name
                }).ToList();
                return listNotif;
            }
            catch (Exception)
            {
                throw new Exception("Error while get All Notification Promo");
            }
        }

        public async Task<NotifResponse> UpdateIsReadStatus(Guid notifId)
        {
            try
            {
                var isRead = await _notifReadService.GetOrSaveNotifRead(true);
                var updateIsRead = await _repository.Find(n => n.Id.Equals(notifId), new[] { "CategoryNotification" });
                if (updateIsRead == null) throw new NotFoundException("Notif not Found");
                updateIsRead.NotifReadId = isRead.Id;

                var result = _repository.Update(updateIsRead);
                await _dbPersistence.SaveChangesAsync();

                return new NotifResponse
                {
                    Id = result.Id.ToString(),
                    Title = result.Title,
                    Body = result.Body,
                    CreatedAt = result.CreatedAt.ToString(),
                    isRead = isRead.IsRead,
                    Category = updateIsRead?.CategoryNotification?.Name
                };
            }
            catch (Exception)
            {
                throw new Exception("Error while Update status IsRead Notification");
            }
        }
    }
}
