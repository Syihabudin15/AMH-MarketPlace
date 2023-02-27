using AMH_MarketPlace.Entities.Store.Product;

namespace AMH_MarketPlace.Services.Interface.StoreInterface.ProductInterface
{
    public interface IProductImageService
    {
        Task<List<ProductImage>> SaveProductImage(List<IFormFile> images, Guid productId);
        Task<List<ProductImage>> GetAllProductImage(Guid productId);
        Task<List<string>> DeleteImage(Guid id);
    }
}
