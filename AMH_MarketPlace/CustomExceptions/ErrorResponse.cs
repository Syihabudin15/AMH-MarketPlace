namespace AMH_MarketPlace.CustomExceptions
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string[]? Message { get; set; }
    }
}
