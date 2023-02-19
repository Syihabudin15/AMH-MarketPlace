using AMH_MarketPlace.DTOs.StoreDto;
using AMH_MarketPlace.Entities.Store;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.StoreInterface;
using AMH_MarketPlace.Services.Interface.UserInterface;
using AMH_MarketPlace.CustomExceptions.ExceptionsHandlers;

namespace AMH_MarketPlace.Services.Implement.StoreImplement
{
    public class StoreService : IStoreService
    {
        private readonly IRepository<Store> _repository;
        private readonly IDbPersistence _dbPersistence;
        private readonly IUserService _userService;
        private readonly IRateService _rateService;
        private readonly IStoreImageService _storeImageService;
        public StoreService(
            IRepository<Store> repository,
            IDbPersistence dbPersistence,
            IUserService userService,
            IRateService rateService,
            IStoreImageService storeImageService)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
            _userService = userService;
            _rateService = rateService;
            _storeImageService = storeImageService;
        }

        public async Task<StoreResponse> CreateStore(StoreRequest req, string email)
        {
            try
            {
                var saveStore = await _dbPersistence.ExecuteTransactionAsync(async () =>
                {
                    var user = await _userService.GetMyUser(email);
                    if (user == null) throw new NotFoundException("User not found");

                    var image = await _storeImageService.SaveStoreImage(req.StoreImage);
                    var rate = await _rateService.CreateRateStore(new RateStore());

                    var saveStore = await _repository.Save(new Store
                    {
                        Name = req.Name,
                        Description = req.Description,
                        StoreImageId = image.Id,
                        RateStoreId = rate.Id,
                        UserId = Guid.Parse(user.Id)
                    });
                    await _dbPersistence.SaveChangesAsync();

                    return new StoreResponse
                    {
                        Id = saveStore.Id.ToString(),
                        Name = saveStore.Name,
                        Description = saveStore.Description,
                        StoreImage = image,
                        RateStore = rate,
                        RatioRate = 0,
                        UserResponse = user,
                    };
                });
                return saveStore;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<StoreResponse> GetMyStore(string email)
        {
            try
            {
                var user = await _userService.GetMyUser(email);
                if (user == null) throw new NotFoundException("User not found");

                var storeFind = await _repository.Find(s => s.UserId.Equals(Guid.Parse(user.Id)), new[] { "StoreImage", "RateStore" });
                if (storeFind == null) throw new NotFoundException("You not have Store, Create first");

                var s = storeFind.RateStore;
                //var totalS = s.Rate1 + s.Rate2 + s.Rate3 + s.Rate4 + s.Rate4 + s.Rate5;

                //var ratio =
                //    ((s.Rate1 * 1)
                //    + (s.Rate2 * 2)
                //    + (s.Rate3 * 3)
                //    + (s.Rate4 * 4)
                //    + (s.Rate5 * 5)) / (totalS);
                return new StoreResponse
                {
                    Id = storeFind.Id.ToString(),
                    Name = storeFind.Name,
                    Description = storeFind.Description,
                    StoreImage = storeFind.StoreImage,
                    RateStore = storeFind.RateStore,
                    RatioRate = 1,
                    UserResponse = user
                };
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<RateStore> UpdateRateStore(int numberRate, string id)
        {
            try
            {
                var rateUpdate = await _rateService.UpdateRateStore(numberRate, id);
                return rateUpdate;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<StoreResponse> UpdateStore(StoreRequest req, string email)
        {
            try
            {
                var userFind = await _userService.GetMyUser(email);
                var storeFind = await _repository.Find(s => s.UserId.Equals(Guid.Parse(userFind.Id)), new[] {"StoreImage", "RateStore"});

                var imageUpdate = await _storeImageService.UpdateStoreImage(req.StoreImage, storeFind.StoreImageId);
                var update = new Store
                {
                    Id = storeFind.Id,
                    Name = req.Name,
                    Description = req.Description,
                    StoreImage = imageUpdate,
                    StoreImageId = storeFind.StoreImageId,
                    RateStore = storeFind.RateStore,
                    RateStoreId = storeFind.RateStoreId,
                    UserId = storeFind.UserId
                };
                var storeUpdate = _repository.Update(update);

                var s = storeFind.RateStore;
                var totalS = s.Rate1 + s.Rate2 + s.Rate3 + s.Rate4 + s.Rate4 + s.Rate5;

                var ratio =
                    ((s.Rate1 * 1)
                    + (s.Rate2 * 2)
                    + (s.Rate3 * 3)
                    + (s.Rate4 * 4)
                    + (s.Rate5 * 5)) / (totalS);
                return new StoreResponse
                {
                    Id = storeUpdate.Id.ToString(),
                    Name = storeUpdate.Name,
                    Description = storeUpdate.Description,
                    StoreImage = storeUpdate.StoreImage,
                    RateStore = storeUpdate.RateStore,
                    RatioRate = (decimal)ratio,
                    UserResponse = userFind,
                };
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
