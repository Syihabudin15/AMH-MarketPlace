using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;

namespace AMH_MarketPlace.DTOs.StoreDto.ProductDto
{
    public class ProductRequest
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CategoryProduct { get; set; } = null!;
        public float? WeightKg { get; set; }
        public float? WeightG { get; set; }
        public float? SizeInch { get; set; }
        public float? SizeCm { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }
        public List<IFormFile> ProductImage { get; set; } = null!;
    }
}
