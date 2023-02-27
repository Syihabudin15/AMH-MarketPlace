using AMH_MarketPlace.DTOs.TransactionDto.Purchase;

namespace AMH_MarketPlace.Services.Interface.TransactionInterface.PurchaseProduct
{
    public interface IPurchaseProductServie
    {
        Task<object> PurchaseViaBank(PurchaseViaBankTransfer purchase, string email);
        Task<object> PurchaseViaGopay(PurchaseViaEWallet purchase, string email);

    }
}
