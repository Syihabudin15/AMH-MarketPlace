namespace AMH_MarketPlace.DTOs.BankDto.WalletDto
{
    public class WalletRequest
    {
        public string Name { get; set; } = null!;
        public string NIK { get; set; } = null!;
        public string BirthDate { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string National { get; set; } = null!;
    }
}
