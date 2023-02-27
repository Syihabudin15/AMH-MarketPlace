using AMH_MarketPlace.Entities.Transaction;

namespace AMH_MarketPlace.Services.Interface.TransactionInterface
{
    public interface ITransactionService
    {
        Task<Transaction> CreateTransaction(Transaction transaction);
    }
}
