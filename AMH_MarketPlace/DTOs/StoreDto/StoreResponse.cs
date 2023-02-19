using AMH_MarketPlace.DTOs.UserDto;
using AMH_MarketPlace.Entities.Store;

namespace AMH_MarketPlace.DTOs.StoreDto
{
    public class StoreResponse
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public StoreImage StoreImage { get; set; } = null!;
        public UserResponse UserResponse { get; set; } = null!;
        public RateStore RateStore { get; set; } = null!;
        public decimal RatioRate { get; set; }
    }
}
