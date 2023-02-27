using AMH_MarketPlace.Entities.Bank.Wallet;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.Transaction.TransacWallet
{
    [Table("m_topup_wallet")]
    public class TopUpWallet
    {
        [Key,Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "transaction_id")] public Guid TransactionId { get; set; }
        [Column(name: "wallet_id")] public Guid WalletId { get; set; }
        [Column(name: "amount")] public long Amount { get; set; }
        [Column(name: "status", TypeName = "Varchar(10)")] public string Status { get; set; } = null!;

        public virtual Transaction? Transaction { get; set; }
        public virtual Wallet? Wallet { get; set; }
    }
}
