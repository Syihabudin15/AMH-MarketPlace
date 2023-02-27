using AMH_MarketPlace.DTOs.TransactionDto.Purchase.Utils;

namespace AMH_MarketPlace.DTOs.TransactionDto.Purchase
{
    public class PurchaseViaEWallet
    {
        public string PaymentType { get; set; } = null!;
        public List<UtilProduct> Products { get; set; } = null!;
    }
}
