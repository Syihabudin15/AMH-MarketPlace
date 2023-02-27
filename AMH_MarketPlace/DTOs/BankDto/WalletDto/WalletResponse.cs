namespace AMH_MarketPlace.DTOs.BankDto.WalletDto
{
    public class WalletResponse
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string NoWallet { get; set; } = null!;
        public long Balance { get; set; }
    }
}
