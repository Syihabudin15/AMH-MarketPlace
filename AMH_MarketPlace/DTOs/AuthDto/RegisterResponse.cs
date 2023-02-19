namespace AMH_MarketPlace.DTOs.AuthDto
{
    public class RegisterResponse
    {
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
