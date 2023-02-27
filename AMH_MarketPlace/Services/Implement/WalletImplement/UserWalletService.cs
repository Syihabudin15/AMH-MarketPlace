using AMH_MarketPlace.CustomExceptions.ExceptionsHandlers;
using AMH_MarketPlace.DTOs.BankDto.WalletDto;
using AMH_MarketPlace.Entities.Bank.Wallet;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.UserInterface;
using AMH_MarketPlace.Services.Interface.WalletInterface;
using AMH_MarketPlace.Validations;

namespace AMH_MarketPlace.Services.Implement.WalletImplement
{
    public class UserWalletService : IUserWalletService
    {
        private readonly IRepository<UserWallet> _repository;
        private readonly IDbPersistence _dbPersistence;
        private readonly IUserService _userService;
        private readonly IWalletService _walletService;
        public UserWalletService(
            IRepository<UserWallet> repository,
            IDbPersistence dbPersistence,
            IUserService userService,
            IWalletService walletService)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
            _userService = userService;
            _walletService = walletService;
        }

        public async Task<WalletResponse> CreateUserWallet(WalletRequest wallet, string email)
        {
            try
            {
                var findUser = await _userService.GetMyUser(email);
                var walletFind = await _repository.Find(w => w.UserId.Equals(Guid.Parse(findUser.Id)), new[] { "Wallet" });
                // Jika user sudah punya wallet maka kirim response dengan data yg sudah ada
                if (walletFind != null) return new WalletResponse
                {
                    Id = walletFind.Id.ToString(),
                    Name = walletFind.Name,
                    NoWallet = walletFind.Wallet.NoWallet,
                    Balance = walletFind.Wallet.Balance
                };
                // Jika user belum punya wallet maka buat disini
                var saveWallet = await _walletService.CreateWallet(new Wallet
                {
                    NoWallet = "1138" + findUser.PhoneNumber,
                    Balance = 0
                });
                var saveUserWallet = await _repository.Save(new UserWallet
                {
                    Name = wallet.Name,
                    NIK = wallet.NIK,
                    Address = wallet.Address,
                    BirthDate = DateTime.Parse(wallet.BirthDate),
                    City = wallet.City,
                    National = wallet.National,
                    IsVerified = false,
                    UserId = Guid.Parse(findUser.Id),
                    WalletId = saveWallet.Id
                });
                await _dbPersistence.SaveChangesAsync();

                return new WalletResponse
                {
                    Id = saveUserWallet.Id.ToString(),
                    Name = saveUserWallet.Name,
                    NoWallet = saveWallet.NoWallet,
                    Balance = saveWallet.Balance
                };
            }
            catch (Exception)
            {
                throw new Exception("Error while Create User Wallet");
            }
        }

        public async Task<WalletResponse> GetMyWallet(string email)
        {
            try
            {
                var user = await _userService.GetMyUser(email);
                var walletFind = await _repository.Find(w => w.UserId.Equals(Guid.Parse(user.Id)), new[] { "Wallet" });
                if (walletFind == null) throw new NotFoundException("Youre not have a Wallet. Create a Wallet is Free");
                return new WalletResponse
                {
                    Id = walletFind.Id.ToString(),
                    Name = walletFind.Name,
                    NoWallet = walletFind.Wallet.NoWallet,
                    Balance = (long)walletFind.Wallet.Balance
                };
            }
            catch (Exception)
            {
                throw new Exception("Error while get My Wallet");
            }
        }

        public async Task<WalletResponse> UpdateMyWallet(WalletRequest wallet, string email)
        {
            try
            {
                if (!ValidateRequest.ValidateNull(wallet.Name) ||
                    !ValidateRequest.ValidateNull(wallet.NIK) ||
                    !ValidateRequest.ValidateNull(wallet.Address) ||
                    !ValidateRequest.ValidateNull(wallet.National) ||
                    !ValidateRequest.ValidateNull(wallet.City) ||
                    !ValidateRequest.ValidateNull(wallet.BirthDate)
                    ) throw new NotNullException(new[]
                    {
                        "Field Name cannot be null",
                        "Field NIK cannot be null",
                        "Field Address cannot be null",
                        "Field National cannot be null",
                        "Field City cannot be null",
                        "Field BirthDate cannot be null"
                    });

                var userFind = await _userService.GetMyUser(email);
                var walletFind = await _repository.Find(w => w.UserId.Equals(Guid.Parse(userFind.Id)), new[] { "Wallet" });
                if (walletFind == null) throw new NotFoundException("Youre not have a wallet");
                var transacUpdateWallet = await _dbPersistence.ExecuteTransactionAsync(async () =>
                {
                    walletFind.Name = wallet.Name;
                    walletFind.NIK = wallet.NIK;
                    walletFind.Address = wallet.Address;
                    walletFind.City = wallet.City;
                    walletFind.National = wallet.National;
                    walletFind.BirthDate = DateTime.Parse(wallet.BirthDate);

                    var updateUserWallet = _repository.Update(walletFind);
                    await _dbPersistence.SaveChangesAsync();

                    return new WalletResponse
                    {
                        Id = updateUserWallet.Id.ToString(),
                        Name = updateUserWallet.Name,
                        NoWallet = updateUserWallet.Wallet.NoWallet,
                        Balance = updateUserWallet.Wallet.Balance
                    };
                });

                return transacUpdateWallet;
            }
            catch (Exception)
            {
                throw new Exception("Error whilie Update Wallet");
            }
        }

        public async Task<WalletResponse> VerifyUserWallet(string id)
        {
            try
            {
                var walletFind = await _repository.Find(w => w.Id.Equals(Guid.Parse(id)), new[] { "Wallet" });
                if (walletFind == null) throw new NotFoundException("Wallet Not Found");
                walletFind.IsVerified = true;
                await _dbPersistence.SaveChangesAsync();
                return new WalletResponse
                {
                    Id = walletFind.Id.ToString(),
                    Name = walletFind.Name,
                    NoWallet = walletFind.Wallet.NoWallet,
                    Balance = walletFind.Wallet.Balance
                };
            }
            catch (Exception)
            {
                throw new Exception("Error while verify User Wallet");
            }
        }
    }
}
