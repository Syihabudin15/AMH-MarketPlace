using AMH_MarketPlace.Entities.Bank.Wallet;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.WalletInterface;
using AMH_MarketPlace.CustomExceptions.ExceptionsHandlers;

namespace AMH_MarketPlace.Services.Implement.WalletImplement
{
    public class WalletService : IWalletService
    {
        private readonly IRepository<Wallet> _repository;
        private readonly IDbPersistence _dbPersistence;
        public WalletService(IRepository<Wallet> repository, IDbPersistence dbPersistence)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
        }

        public async Task<Wallet> CreateWallet(Wallet wallet)
        {
            var save = await _repository.Save(wallet);
            await _dbPersistence.SaveChangesAsync();
            return save;
        }
        public async Task<Wallet> UpdateBalance(Wallet wallet)
        {
            try
            {
                var walletFind = await _repository.FindById(wallet.Id);
                if (walletFind == null) throw new NotFoundException("Wallet Not Found");
                walletFind.Balance = wallet.Balance;
                var saveUpdate = _repository.Update(walletFind);
                return saveUpdate;
            }
            catch (Exception)
            {
                throw new Exception("Error While Update Balance");
            }
        }
    }
}
