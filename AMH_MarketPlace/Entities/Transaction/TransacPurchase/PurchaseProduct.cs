using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.Transaction.TransacPurchase
{
    [Table("m_purchase_product")]
    public class PurchaseProduct
    {
        [Key,Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "transaction_id")] public Guid TransactionId { get; set; }
        [Column(name: "product_id")] public Guid ProductId { get; set; }
        [Column(name: "quantity")] public int Quantity { get; set; }
    }
}
