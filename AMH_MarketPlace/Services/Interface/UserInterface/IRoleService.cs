using AMH_MarketPlace.Entities.User;

namespace AMH_MarketPlace.Services.Interface.UserInterface
{
    public interface IRoleService
    {
        Task<Role> GetOrSaveRole(string role);
    }
}
