namespace AMH_MarketPlace.DTOs
{
    public class PageResponse<T>
    {
        public int TotalPage { get; set; }
        public int TotalElement { get; set; }
        public List<T> Content { get; set; } = null!;
    }
}
