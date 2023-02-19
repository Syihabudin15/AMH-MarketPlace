namespace AMH_MarketPlace.DTOs.AuthDto
{
    public class LoginResponse
    {
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
