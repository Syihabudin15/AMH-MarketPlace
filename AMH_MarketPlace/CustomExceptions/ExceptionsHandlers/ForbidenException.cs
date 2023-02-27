namespace AMH_MarketPlace.CustomExceptions.ExceptionsHandlers
{
    public class ForbidenException : Exception
    {
        public ForbidenException(string? message) : base(message)
        {
        }
    }
}
