using AMH_MarketPlace.CustomExceptions.ExceptionsHandlers;
using AMH_MarketPlace.DTOs;
using AMH_MarketPlace.DTOs.StoreDto.ProductDto;
using AMH_MarketPlace.Entities.Store.Product;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.StoreInterface;
using AMH_MarketPlace.Services.Interface.StoreInterface.ProductInterface;
using AMH_MarketPlace.Validations;

namespace AMH_MarketPlace.Services.Implement.StoreImplement.ProductImplement
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IDbPersistence _dbPersistence;
        private readonly ICategoryProductService _categoryProductService;
        private readonly IProductImageService _productImageService;
        private readonly IStoreService _storeService;
        public ProductService(
            IRepository<Product> repository,
            IDbPersistence dbPersistence,
            ICategoryProductService categoryProductService,
            IProductImageService productImageService,
            IStoreService storeService)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
            _categoryProductService = categoryProductService;
            _productImageService = productImageService;
            _storeService = storeService;
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest req, string email)
        {
            try
            {
                var getMyStore = await _storeService.GetMyStore(email);
                if (getMyStore == null) throw new ForbidenException("Youre not have Store. Please Create sttore first");
                if (!ValidateRequest.ValidateNull(req.Name) ||
                    !ValidateRequest.ValidateNull(req.CategoryProduct) ||
                    !ValidateRequest.ValidateNull(req.Description) ||
                    req.Price <= 0 || req.Stock <= 0) throw new NotNullException(new[]
                    {
                        "Product Name cannot be null",
                        "Product Description cannot be null",
                        "product Category cannot be null",
                        "Price not valid",
                        "Stock must be Higher than 0"
                    });
                var transacCreateProduct =
                    await _dbPersistence.ExecuteTransactionAsync(async () =>
                    {
                        var categoryProdyct = await _categoryProductService.GetOrSaveCategoryProduct(req.CategoryProduct);

                        Product product = new()
                        {
                            Name = req.Name,
                            Description = req.Description,
                            SizeCm = req.SizeCm,
                            SizeInch = req.SizeInch,
                            WeightKg = req.WeightKg,
                            WeightG = req.WeightG,
                            Price = req.Price,
                            Stock = req.Stock,
                            StoreId = Guid.Parse(getMyStore.Id),
                            CategoryProductId = categoryProdyct.Id
                        };
                        var saveProduct = await _repository.Save(product);
                        var saveImage = await _productImageService.SaveProductImage(req.ProductImage, saveProduct.Id);
                        await _dbPersistence.SaveChangesAsync();

                        return new ProductResponse
                        {
                            Id = saveProduct.Id.ToString(),
                            ProductName = saveProduct.Name,
                            ProductDescription = saveProduct.Description,
                            Weight = saveProduct.WeightKg != null ? $"{saveProduct.WeightKg} Kg" : $"{saveProduct.WeightG} G",
                            Size = saveProduct.SizeInch != null ? $"{saveProduct.SizeInch} Inch" : $"{saveProduct.SizeCm} Cm",
                            Price = saveProduct.Price,
                            Stock = saveProduct.Stock,
                            StoreName = getMyStore.Name,
                            ProductImages = saveImage
                        };
                    });
                return transacCreateProduct;
            }
            catch (Exception)
            {
                throw new Exception("Error While Create A Product");
            }
        }

        public async Task<List<string>> DeleteProduct(string id)
        {
            try
            {
                if (id == null || id == "" || id == " ") throw new NotNullException("Id cannot be null");
                var findProduct = await _repository.FindById(Guid.Parse(id));
                if (findProduct == null) throw new NotFoundException("Product not Found");

                var deleteImage = await _productImageService.DeleteImage(findProduct.Id);
                _repository.Delete(findProduct);
                await _dbPersistence.SaveChangesAsync();

                return deleteImage;
            }
            catch (Exception)
            {
                throw new Exception("Error while Delete Product");
            }
        }

        public async Task<PageResponse<ProductResponse>> GetAllProduct(int page, int size)
        {
            try
            {
                var findProducts = await _repository.FindAll(p => true, page, size, new[] { "Store" });
                List<ProductResponse> results = new List<ProductResponse>();

                foreach (var p in findProducts)
                {
                    var findImages = await _productImageService.GetAllProductImage(p.Id);
                    var result = new ProductResponse
                    {
                        Id = p.Id.ToString(),
                        ProductName = p.Name,
                        ProductDescription = p.Description,
                        Weight = p.WeightKg != null ? $"{p.WeightKg} Kg" : $"{p.WeightG} G",
                        Size = p.SizeInch != null ? $"{p.SizeInch} Inch" : $"{p.SizeCm} Cm",
                        Price = p.Price,
                        Stock = p.Stock,
                        StoreName = p.Store.Name,
                        ProductImages = findImages
                    };
                    results.Add(result);
                }
                var totalPage = (int)Math.Ceiling(await _repository.Count() / (decimal)size);
                PageResponse<ProductResponse> response = new()
                {
                    TotalPage = totalPage,
                    TotalElement = findProducts.Count(),
                    Content = results
                };
                return response;

            }
            catch (Exception)
            {
                throw new Exception("Error while get all Product");
            }
        }

        public async Task<Product> GetForTransaction(string id)
        {
            try
            {
                var findProduct = await _repository.Find(p => p.Id.Equals(Guid.Parse(id)));
                if (findProduct == null) throw new NotFoundException("Product not found");

                return findProduct;
            }
            catch (Exception)
            {
                throw new Exception("Error while get Product By Id");
            }
        }

        public async Task<PageResponse<ProductResponse>> GetMyProduct(string email, int page, int size)
        {
            try
            {
                var getStore = await _storeService.GetMyStore(email);
                var findProduct = await _repository.FindAll(p => p.StoreId.Equals(Guid.Parse(getStore.Id)), page, size);

                List<ProductResponse> results = new List<ProductResponse>();

                foreach (var product in findProduct)
                {
                    var findImages = await _productImageService.GetAllProductImage(product.Id);
                    var result = new ProductResponse
                    {
                        Id = product.Id.ToString(),
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        Weight = product.WeightKg != null ? $"{product.WeightKg} Kg" : $"{product.WeightG} G",
                        Size = product.SizeInch != null ? $"{product.SizeInch} Inch" : $"{product.SizeCm} Cm",
                        Price = product.Price,
                        Stock = product.Stock,
                        StoreName = getStore.Name,
                        ProductImages = findImages
                    };
                    results.Add(result);
                }
                var totalPage = (int)Math.Ceiling(await _repository.Count() / (decimal)size);
                PageResponse<ProductResponse> response = new()
                {
                    TotalPage = totalPage,
                    TotalElement = findProduct.Count(),
                    Content = results
                };
                return response
;
            }
            catch (Exception)
            {
                throw new Exception("Error while get My Product");
            }
        }

        public async Task<ProductResponse> GetProductById(string id)
        {
            try
            {
                var findProduct = await _repository.Find(p => p.Id.Equals(Guid.Parse(id)), new[] { "Store" });
                var findImages = await _productImageService.GetAllProductImage(findProduct.Id);
                if (findProduct == null) throw new NotFoundException("Product Not Found");
                return new ProductResponse
                {
                    Id = findProduct.Id.ToString(),
                    ProductName = findProduct.Name,
                    ProductDescription = findProduct.Description,
                    Weight = findProduct.WeightKg != null ? $"{findProduct.WeightKg} Kg" : $"{findProduct.WeightG} G",
                    Size = findProduct.SizeInch != null ? $"{findProduct.SizeInch} Inch" : $"{findProduct.SizeCm} Cm",
                    Price = findProduct.Price,
                    Stock = findProduct.Stock,
                    StoreName = findProduct.Store.Name,
                    ProductImages = findImages
                };
            }
            catch (Exception)
            {
                throw new Exception("Error While Get Product By Id");
            }
        }
    }
}
