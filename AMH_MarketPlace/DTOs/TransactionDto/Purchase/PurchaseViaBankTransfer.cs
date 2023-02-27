using AMH_MarketPlace.DTOs.TransactionDto.Purchase.Utils;

namespace AMH_MarketPlace.DTOs.TransactionDto.Purchase
{
    public class PurchaseViaBankTransfer
    {
        public string BankName { get; set; } = null!;
        public List<UtilProduct> Product { get; set; } = null!;
    }
}
