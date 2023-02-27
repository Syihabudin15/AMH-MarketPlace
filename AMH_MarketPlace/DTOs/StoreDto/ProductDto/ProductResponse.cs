using AMH_MarketPlace.Entities.Store.Product;

namespace AMH_MarketPlace.DTOs.StoreDto.ProductDto
{
    public class ProductResponse
    {
        public string Id { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public string? Weight { get; set; }
        public string? Size { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }
        public string StoreName { get; set; } = null!;
        public List<ProductImage> ProductImages { get; set; } = null!;
    }
}
