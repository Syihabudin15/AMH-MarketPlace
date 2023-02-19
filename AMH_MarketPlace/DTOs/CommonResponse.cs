namespace AMH_MarketPlace.DTOs
{
    public class CommonResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
        public T? Data { get; set; }
    }
}
