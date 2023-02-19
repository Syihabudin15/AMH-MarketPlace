namespace AMH_MarketPlace.CustomExceptions.ExceptionsHandlers
{
    public class UnAuthorizeException : Exception
    {
        public UnAuthorizeException()
        {
        }

        public UnAuthorizeException(string? message) : base(message)
        {
        }
    }
}
