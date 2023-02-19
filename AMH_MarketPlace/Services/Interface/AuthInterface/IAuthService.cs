using AMH_MarketPlace.DTOs.AuthDto;

namespace AMH_MarketPlace.Services.Interface.AuthInterface
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterUser(RegisterRequest request);
        Task<LoginResponse> Login(LoginRequest request);
    }
}
