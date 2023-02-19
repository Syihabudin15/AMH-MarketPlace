using AMH_MarketPlace.Entities.User.SubUser.Notifications;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.UserInterface.NotifInterface;

namespace AMH_MarketPlace.Services.Implement.UserImplement.NotifImplement
{
    public class NotifReadService : INotifReadService
    {
        private readonly IRepository<NotifRead> _repository;
        private readonly IDbPersistence _dbPersistence;
        public NotifReadService(IRepository<NotifRead> repository, IDbPersistence dbPersistence)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
        }

        public async Task<NotifRead> GetOrSaveNotifRead(bool isRead)
        {
            try
            {
                var isReadFind = await _repository.Find(r => r.IsRead.Equals(isRead));
                if (isReadFind != null) return isReadFind;

                var saveIsRead = await _repository.Save(new NotifRead { IsRead = isRead });
                return saveIsRead;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
