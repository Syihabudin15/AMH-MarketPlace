using AMH_MarketPlace.Entities.Store;

namespace AMH_MarketPlace.Services.Interface.StoreInterface
{
    public interface IRateService
    {
        Task<RateStore> CreateRateStore(RateStore rateStore);
        Task<RateStore> UpdateRateStore(int numberRate, string id);
    }
}
