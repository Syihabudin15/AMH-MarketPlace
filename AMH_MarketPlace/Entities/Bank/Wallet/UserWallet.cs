using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.Bank.Wallet
{
    [Table("m_user_wallet")]
    public class UserWallet
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "name", TypeName = "Varchar(150)")] public string Name { get; set; } = null!;
        [Column(name: "nik", TypeName = "Varchar(16)")] public string? NIK { get; set; }
        [Column(name: "birth_date")] public DateTime? BirthDate { get; set; }
        [Column(name: "address")] public string Address { get; set; } = null!;
        [Column(name: "city", TypeName = "Varchar(100)")] public string City { get; set; } = null!;
        [Column(name: "national", TypeName = "Varchar(100)")] public string? National { get; set; }
        [Column(name: "is_verified")] public bool IsVerified { get; set; }
        [Column(name: "wallet_id")] public Guid WalletId { get; set; }
        [Column(name: "user_id")] public Guid UserId { get; set; }

        public virtual Wallet Wallet { get; set; } = null!;
    }
}
