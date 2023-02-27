using AMH_MarketPlace.Entities.Store.Product;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.StoreInterface.ProductInterface;

namespace AMH_MarketPlace.Services.Implement.StoreImplement.ProductImplement
{
    public class CategoryProductService : ICategoryProductService
    {
        private readonly IRepository<CategoryProduct> _repository;
        private readonly IDbPersistence _dbPersistence;
        public CategoryProductService(IRepository<CategoryProduct> repository, IDbPersistence dbPersistence)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
        }

        public async Task<List<CategoryProduct>> GetAllCategories()
        {
            try
            {
                var results = await _repository.FindAll();
                return results.ToList();
            }
            catch (Exception)
            {
                throw new Exception("Error While getting all Category Product");
            }
        }

        public async Task<CategoryProduct> GetOrSaveCategoryProduct(string categoryName)
        {
            var categoryProductFind = await _repository.Find(c => c.Name.Equals(categoryName));
            if (categoryProductFind != null) return categoryProductFind;

            var saveCategoryProduct = await _repository.Save(new CategoryProduct { Name = categoryName });
            await _dbPersistence.SaveChangesAsync();
            return saveCategoryProduct;
        }
    }
}
