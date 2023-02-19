using AMH_MarketPlace.Entities.User.SubUser.Notifications;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.UserInterface.NotifInterface;

namespace AMH_MarketPlace.Services.Implement.UserImplement.NotifImplement
{
    public class CategoryNotifService : ICategoryNotifService
    {
        private readonly IRepository<CategoryNotification> _repository;
        private readonly IDbPersistence _dbPersistence;
        public CategoryNotifService(IRepository<CategoryNotification> repository, IDbPersistence dbPersistence)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
        }

        public async Task<CategoryNotification> GetOrSaveCategoryNotif(string notif)
        {
            try
            {
                var notifFind = await _repository.Find(n => n.Name.Equals(notif));
                if (notifFind != null) return notifFind;

                var saveCategorNotif = await _repository.Save(new CategoryNotification { Name = notif});
                await _dbPersistence.SaveChangesAsync();

                return saveCategorNotif;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
