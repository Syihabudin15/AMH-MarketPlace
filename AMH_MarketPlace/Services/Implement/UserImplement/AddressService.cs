using AMH_MarketPlace.CustomExceptions.ExceptionsHandlers;
using AMH_MarketPlace.DTOs.UserDto;
using AMH_MarketPlace.Entities.User.SubUser;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.UserInterface;

namespace AMH_MarketPlace.Services.Implement.UserImplement
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> _repository;
        private readonly IDbPersistence _dbPersistence;
        public AddressService(IRepository<Address> repository, IDbPersistence dbPersistence)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
        }

        public async Task<Address> CreateAddress(Guid userId)
        {
            try
            {
                var saveAddress = await _repository.Save(new Address { UserId = userId });
                await _dbPersistence.SaveChangesAsync();
                return saveAddress;
            }
            catch (Exception)
            {
                throw new Exception("Error while Create User Address");
            }
        }

        public async Task<AddressResponse> GetMyAddress(Guid userId)
        {
            try
            {
                var myAddress = await _repository.Find(a => a.UserId.Equals(userId));
                if (myAddress == null) throw new NotFoundException("Address not found");
                return new AddressResponse
                {
                    Id = myAddress.ToString(),
                    Address1 = myAddress.Address1,
                    Address2 = myAddress.Address2,
                    City = myAddress.City,
                    PostCode = myAddress.PostCode
                };
            }
            catch (Exception)
            {
                throw new Exception("Error get My User Address");
            }
        }

        public async Task<AddressResponse> UpdateAddress(AddressRequest address, Guid userId)
        {
            try
            {
                var addressFind = await _repository.Find(a => a.UserId.Equals(userId));
                addressFind.Address1 = address.Address1 != null ? address.Address1 : addressFind.Address1;
                addressFind.Address2 = address.Address2 != null ? address.Address2 : addressFind.Address2;
                addressFind.City = address.City != null ? address.City : addressFind.City;
                addressFind.PostCode = address.PostCode != null ? address.PostCode : addressFind.PostCode;

                var addressEdited = _repository.Update(addressFind);
                await _dbPersistence.SaveChangesAsync();

                return new AddressResponse
                {
                    Id = addressFind.Id.ToString(),
                    Address1 = addressFind.Address1,
                    Address2 = addressFind.Address2,
                    City = addressFind.City,
                    PostCode = addressFind.PostCode
                };
            }
            catch (Exception)
            {
                throw new Exception("Error while update My User Address");
            }
        }
    }
}
