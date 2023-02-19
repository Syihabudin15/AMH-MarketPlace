namespace AMH_MarketPlace.DTOs.UserDto
{
    public class UserResponse
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public AddressResponse Address { get; set; } = null!;
    }
}
