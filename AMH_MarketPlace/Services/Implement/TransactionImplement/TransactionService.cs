using AMH_MarketPlace.Entities.Transaction;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.TransactionInterface;

namespace AMH_MarketPlace.Services.Implement.TransactionImplement
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _repository;
        private readonly IDbPersistence _dbPersistence;
        public TransactionService(IRepository<Transaction> repository, IDbPersistence dbPersistence)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
        }

        public async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            var save = await _repository.Save(transaction);
            await _dbPersistence.SaveChangesAsync();
            return transaction;
        }
    }
}
