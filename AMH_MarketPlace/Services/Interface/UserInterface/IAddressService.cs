using AMH_MarketPlace.DTOs.UserDto;
using AMH_MarketPlace.Entities.User.SubUser;

namespace AMH_MarketPlace.Services.Interface.UserInterface
{
    public interface IAddressService
    {
        Task<Address> CreateAddress(Guid userId);
        Task<AddressResponse> UpdateAddress(AddressRequest address, Guid userId);
        Task<AddressResponse> GetMyAddress(Guid userId);
    }
}
