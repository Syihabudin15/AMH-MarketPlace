namespace AMH_MarketPlace.CustomExceptions.ExceptionsHandlers
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string? message) : base(message)
        {
        }
    }
}
