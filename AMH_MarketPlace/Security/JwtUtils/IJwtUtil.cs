using AMH_MarketPlace.Entities.User;

namespace AMH_MarketPlace.Security.JwtUtils
{
    public interface IJwtUtil
    {
        string GenerateToken(Credential credential);
    }
}
