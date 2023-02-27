using AMH_MarketPlace.Entities.Store.Product;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface;
using AMH_MarketPlace.Services.Interface.StoreInterface.ProductInterface;

namespace AMH_MarketPlace.Services.Implement.StoreImplement.ProductImplement
{
    public class ProductImageService : IProductImageService
    {
        private readonly IRepository<ProductImage> _repository;
        private readonly IDbPersistence _dbPersistence;
        private readonly IFileService _fileService;
        public ProductImageService(IRepository<ProductImage> repository, IDbPersistence dbPersistence, IFileService fileService)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
            _fileService = fileService;
        }

        public async Task<List<string>> DeleteImage(Guid id)
        {
            try
            {
                var images = await _repository.FindAll(i => i.ProductId.Equals(id));
                List<string> result = new List<string>();
                foreach(var image in images)
                {
                    var delete = await _fileService.DeleteFile(image.FilePath);
                    result.Add(delete);
                }
                _repository.DeleteAll(images);
                await _dbPersistence.SaveChangesAsync();
                return result;
            }
            catch (Exception)
            {
                throw new Exception("Error while delete Product Images");
            }
        }

        public async Task<List<ProductImage>> GetAllProductImage(Guid productId)
        {
            try
            {
                var images = await _repository.FindAll(i => i.ProductId.Equals(productId));
                return images.ToList();
            }
            catch (Exception)
            {
                throw new Exception("Error get all Product Image");
            }
        }

        public async Task<List<ProductImage>> SaveProductImage(List<IFormFile> images, Guid productId)
        {
            try
            {
                List<ProductImage> results = new List<ProductImage>();
                foreach(var image in images)
                {
                    var filePath = await _fileService.SaveFile(image, "Product");
                    ProductImage productImage = new()
                    {
                        FileName = image.FileName,
                        FileSize = image.Length,
                        FilePath = filePath,
                        ContenType = image.ContentType,
                        ProductId = productId
                    };
                    results.Add(productImage);
                }
                var save = await _repository.SaveAll(results);
                await _dbPersistence.SaveChangesAsync();
                return save.ToList();
            }
            catch (Exception)
            {
                throw new Exception("Error While saving Product Image");
            }
        }
    }
}
