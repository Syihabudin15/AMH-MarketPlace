using AMH_MarketPlace.DTOs.StoreDto;
using AMH_MarketPlace.Entities.Store;

namespace AMH_MarketPlace.Services.Interface.StoreInterface
{
    public interface IStoreService
    {
        Task<StoreResponse> CreateStore(StoreRequest req, string email);
        Task<StoreResponse> GetMyStore(string email);
        Task<StoreResponse> UpdateStore(StoreRequest req, string email);
        Task<RateStore> UpdateRateStore(int numberRate, string id);
    }
}
