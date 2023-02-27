using AMH_MarketPlace.DTOs.BankDto.WalletDto;

namespace AMH_MarketPlace.Services.Interface.WalletInterface
{
    public interface IUserWalletService
    {
        Task<WalletResponse> CreateUserWallet(WalletRequest wallet, string email);
        Task<WalletResponse> GetMyWallet(string email);
        Task<WalletResponse> UpdateMyWallet(WalletRequest wallet, string email);
        Task<WalletResponse> VerifyUserWallet(string id);
    }
}
