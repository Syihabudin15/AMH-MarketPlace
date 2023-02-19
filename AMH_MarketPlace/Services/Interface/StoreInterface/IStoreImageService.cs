using AMH_MarketPlace.Entities.Store;
using Microsoft.AspNetCore.Http;

namespace AMH_MarketPlace.Services.Interface.StoreInterface
{
    public interface IStoreImageService
    {
        Task<StoreImage> SaveStoreImage(IFormFile? file);
        Task<StoreImage> UpdateStoreImage(IFormFile file, Guid id);
    }
}
