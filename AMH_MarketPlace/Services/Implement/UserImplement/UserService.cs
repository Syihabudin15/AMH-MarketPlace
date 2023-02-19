using AMH_MarketPlace.DTOs.UserDto;
using AMH_MarketPlace.Entities.User;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.UserInterface;
using AMH_MarketPlace.Services.Interface.UserInterface.NotifInterface;
using AMH_MarketPlace.DTOs.UserDto.NotificationDto;
using AMH_MarketPlace.Entities.Enum;
using AMH_MarketPlace.CustomExceptions.ExceptionsHandlers;
using Microsoft.AspNetCore.Components.Forms;

namespace AMH_MarketPlace.Services.Implement.UserImplement
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IDbPersistence _dbPersistence;
        private readonly IAddressService _addressService;
        private readonly INotificationService _notificationService;
        public UserService(
            IRepository<User> repository,
            IDbPersistence dbPersistence,
            IAddressService addressService,
            INotificationService notificationService)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
            _addressService = addressService;
            _notificationService = notificationService;
        }

        public async Task<User> CreateUser(User user)
        {
            try
            {
                var save = await _repository.Save(user);
                var address = await _addressService.CreateAddress(save.Id);
                var notif = await _notificationService.CreateNotification(new NotifRequest
                {
                    Title = "You need to Update Address",
                    Body = "After Registration Successfully, you need Update your Address for completing your Data, Go to Setting > Update Personal Info"
                }, save.Id.ToString(), ENotif.Info.ToString());

                await _dbPersistence.SaveChangesAsync();

                return save;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<UserResponse> GetMyUser(string Email)
        {
            try
            {
                var user = await _repository.Find(u => u.Credential.Email.Equals(Email), new[] {"Credential.Role", "Address"});
                if (user == null) throw new NotFoundException("user Not Found");

                return new UserResponse
                {
                    Id = user.Id.ToString(),
                    Name = user.FirstName + " " + user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Credential.Email,
                    Role = user.Credential.Role?.Name,
                    Address = new()
                    {
                        Id = user.Address.Id.ToString(),
                        Address1 = user.Address.Address1,
                        Address2 = user.Address.Address2,
                        City = user.Address.City,
                        PostCode = user.Address.PostCode
                    }
                };
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<UserResponse> UpdateAddress(AddressRequest request, string email)
        {
            try
            {
                var findUser = await _repository.Find(u => u.Credential.Email.Equals(email), new[] {"Credential.Role", "Address"});
                var userAddress = await _addressService.UpdateAddress(request, findUser.Id);
                var notif = await _notificationService.CreateNotification(new NotifRequest
                {
                    Title = "Update Address Success",
                    Body = "Youre Address successfully Updated. Thank You"
                }, findUser.Id.ToString(), ENotif.Info.ToString());
                return new UserResponse
                { 
                    Id = findUser.Id.ToString(),
                    Name = findUser.FirstName + " " + findUser.LastName,
                    PhoneNumber = findUser.PhoneNumber,
                    Email = findUser.Credential?.Email,
                    Address = userAddress,
                    Role = findUser.Credential?.Role?.Name,
                };
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public async Task<List<NotifResponse>> GetMyNotification(string email)
        {
            try
            {
                var userFind = await _repository.Find(u => u.Credential.Email.Equals(email), new[] {"Credential"});
                var listMyNotif = await _notificationService.GetAllMyNotification(userFind.Id);
                return listMyNotif;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
