using AMH_MarketPlace.DTOs.StoreDto;
using AMH_MarketPlace.DTOs.BankDto.WalletDto;
using AMH_MarketPlace.Entities.Store;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.StoreInterface;
using AMH_MarketPlace.Services.Interface.UserInterface;
using AMH_MarketPlace.CustomExceptions.ExceptionsHandlers;
using AMH_MarketPlace.Services.Interface.WalletInterface;

namespace AMH_MarketPlace.Services.Implement.StoreImplement
{
    public class StoreService : IStoreService
    {
        private readonly IRepository<Store> _repository;
        private readonly IDbPersistence _dbPersistence;
        private readonly IUserService _userService;
        private readonly IRateService _rateService;
        private readonly IStoreImageService _storeImageService;
        private readonly IUserWalletService _userWalletService;
        public StoreService(
            IRepository<Store> repository,
            IDbPersistence dbPersistence,
            IUserService userService,
            IRateService rateService,
            IStoreImageService storeImageService,
            IUserWalletService userWalletService)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
            _userService = userService;
            _rateService = rateService;
            _storeImageService = storeImageService;
            _userWalletService = userWalletService;
        }

        public async Task<StoreResponse> CreateStore(StoreRequest req, string email)
        {
            try
            {
                var transacCreateStore = await _dbPersistence.ExecuteTransactionAsync(async () =>
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
                    var saveWallet = 
                    await _userWalletService.CreateUserWallet(new WalletRequest
                    {
                        Name = user.Name,
                        NIK = user.PhoneNumber,
                        Address = user.Address.Address1 + user.Address.Address2,
                        City = user.Address.City != null ? user.Address.City : "My City",
                        BirthDate = "01-02-2000",
                        National = "Indonesia"
                    }, user.Email);

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
                return transacCreateStore;
            }
            catch (Exception)
            {
                throw new Exception("Error while create Store");
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
                var totalS = s.Rate1 + s.Rate2 + s.Rate3 + s.Rate4 + s.Rate5;
                decimal ratio;
                if (totalS == 0)
                {
                    ratio = 0;
                }
                else
                {
                    ratio =
                        (decimal)(((s.Rate1 * 1)
                        + (s.Rate2 * 2)
                        + (s.Rate3 * 3)
                        + (s.Rate4 * 4)
                        + (s.Rate5 * 5)) / totalS);
                }
                return new StoreResponse
                {
                    Id = storeFind.Id.ToString(),
                    Name = storeFind.Name,
                    Description = storeFind.Description,
                    StoreImage = storeFind.StoreImage,
                    RateStore = storeFind.RateStore,
                    RatioRate = ratio,
                    UserResponse = user
                };
            }
            catch (Exception)
            {
                throw new Exception("Error while get my Store");
            }
        }

        public async Task<RateStore> UpdateRateStore(int numberRate, string id)
        {
            try
            {
                var transacUpdateRate = await _dbPersistence.ExecuteTransactionAsync(async () =>
                {
                    var rateUpdate = await _rateService.UpdateRateStore(numberRate, id);
                    return rateUpdate;
                });
                return transacUpdateRate;
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
                if (storeFind == null) throw new NotFoundException("Store not found");
                var transacUpdateStore = await _dbPersistence.ExecuteTransactionAsync(async () =>
                {

                    var image = await _storeImageService.UpdateStoreImage(req.StoreImage, storeFind.StoreImageId);

                    storeFind.Name = req.Name == null ? storeFind.Name : req.Name;
                    storeFind.Description = req.Description == null ? storeFind.Description : req.Description;
                    storeFind.StoreImage = req.StoreImage == null ? storeFind.StoreImage : image;

                    var updateStore = _repository.Update(storeFind);
                    await _dbPersistence.SaveChangesAsync();

                    var s = updateStore.RateStore;
                    var totalS = s.Rate1 + s.Rate2 + s.Rate3 + s.Rate4 + s.Rate5;

                    var ratio = (decimal?)
                        ((s.Rate1 * 1)
                        + (s.Rate2 * 2)
                        + (s.Rate3 * 3)
                        + (s.Rate4 * 4)
                        + (s.Rate5 * 5)) / totalS;
                    return new StoreResponse
                    {
                        Id = updateStore.Id.ToString(),
                        Name = updateStore.Name,
                        Description = updateStore.Description,
                        StoreImage = updateStore.StoreImage,
                        RateStore = updateStore.RateStore,
                        RatioRate = (decimal)ratio,
                        UserResponse = userFind
                    };
                });

                return transacUpdateStore;
            }
            catch (Exception)
            {
                throw new Exception("Error while update Store");
            }
        }
    }
}
