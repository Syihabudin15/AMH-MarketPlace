using AMH_MarketPlace.DTOs.UserDto;
using AMH_MarketPlace.DTOs.UserDto.NotificationDto;
using AMH_MarketPlace.Entities.User;

namespace AMH_MarketPlace.Services.Interface.UserInterface
{
    public interface IUserService
    {
        Task<User> CreateUser(User user);
        Task<UserResponse> GetMyUser(string Email);
        Task<UserResponse> UpdateAddress(AddressRequest request, string email);
        Task<UserResponse> GetById(Guid id);
        Task<List<NotifResponse>> GetMyNotification(string email);
    }
}
