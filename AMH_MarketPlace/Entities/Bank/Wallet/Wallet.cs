using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.Bank.Wallet
{
    [Table("m_wallet")]
    public class Wallet
    {
        [Key,Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "no_wallet", TypeName = "Varchar(17)")] public string NoWallet { get; set; } = null!;
        [Column(name: "balance")] public long Balance { get; set; }
    }
}
