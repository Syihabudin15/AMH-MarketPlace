using AMH_MarketPlace.Entities.Store.Product;

namespace AMH_MarketPlace.Services.Interface.StoreInterface.ProductInterface
{
    public interface ICategoryProductService
    {
        Task<CategoryProduct> GetOrSaveCategoryProduct(string categoryName);
        Task<List<CategoryProduct>> GetAllCategories();
    }
}
