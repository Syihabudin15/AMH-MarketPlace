namespace AMH_MarketPlace.DTOs.UserDto
{
    public class AddressResponse
    {
        public string Id { get; set; } = null!;
        public string Address1 { get; set; } = null!;
        public string Address2 { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PostCode { get; set; } = null!;
    }
}
