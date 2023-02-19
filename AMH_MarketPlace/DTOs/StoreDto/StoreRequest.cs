using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Http;

namespace AMH_MarketPlace.DTOs.StoreDto
{
    public class StoreRequest
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile? StoreImage { get; set; }
    }
}
