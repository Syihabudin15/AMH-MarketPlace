using AMH_MarketPlace.DTOs;
using AMH_MarketPlace.DTOs.StoreDto.ProductDto;
using AMH_MarketPlace.Entities.Store.Product;

namespace AMH_MarketPlace.Services.Interface.StoreInterface.ProductInterface
{
    public interface IProductService
    {
        Task<ProductResponse> CreateProduct(ProductRequest req, string email);
        Task<PageResponse<ProductResponse>> GetAllProduct(int page, int size);
        Task<PageResponse<ProductResponse>> GetMyProduct(string email, int page, int size);
        Task<ProductResponse> GetProductById(string id);
        Task<List<string>> DeleteProduct(string id);
        Task<Product> GetForTransaction(string id);
    }
}
