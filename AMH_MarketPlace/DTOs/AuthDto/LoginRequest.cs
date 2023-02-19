namespace AMH_MarketPlace.DTOs.AuthDto
{
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
