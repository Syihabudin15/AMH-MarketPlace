using AMH_MarketPlace.CustomExceptions.ExceptionsHandlers;
using AMH_MarketPlace.Entities.User.SubUser;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.UserInterface;
using System.Reflection.Metadata;

namespace AMH_MarketPlace.DTOs.UserDto
{
    public class AddressRequest
    {
        public string Address1 { get; set; } = null!;
        public string Address2 { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PostCode { get; set; } = null!;
    }
}
