namespace AMH_MarketPlace.Repositories
{
    public interface IDbPersistence
    {
        Task SaveChangesAsync();
        Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func);
    }
}
