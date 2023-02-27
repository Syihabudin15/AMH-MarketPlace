using AMH_MarketPlace.Entities.Bank.Wallet;

namespace AMH_MarketPlace.Services.Interface.WalletInterface
{
    public interface IWalletService
    {
        Task<Wallet> CreateWallet(Wallet wallet);
        Task<Wallet> UpdateBalance(Wallet wallet);
    }
}
