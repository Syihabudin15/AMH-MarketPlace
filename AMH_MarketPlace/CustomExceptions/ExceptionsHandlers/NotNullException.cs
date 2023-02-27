namespace AMH_MarketPlace.CustomExceptions.ExceptionsHandlers
{
    public class NotNullException : Exception
    {
        public NotNullException(string[] message)
        {
        }

        public NotNullException(string? message) : base(message)
        {
        }
    }
}
