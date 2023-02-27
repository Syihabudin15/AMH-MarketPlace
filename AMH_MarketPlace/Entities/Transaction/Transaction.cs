using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.Transaction
{
    [Table("m_transaction")]
    public class Transaction
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "transaction_date")] public DateTime TransactionDate { get; set; }
        [Column(name: "user_id")] public Guid UserId { get; set; }
        [Column(name: "description", TypeName = "Varchar(50)")] public string Description { get; set; } = null!; 
        [Column(name: "payment_method", TypeName = "Varchar(20)")] public string? PaymentMethod { get; set; }
        [Column(name: "reference_pg", TypeName = "Varchar(50)")] public string? ReferencePg { get; set; }
        [Column(name: "order_id", TypeName = "Varchar(50)")] public string OrderId { get; set; } = null!;
        [Column(name: "status", TypeName = "Varchar(15)")] public string Status { get; set; } = null!;

        public virtual User.User? User { get; set; }
    }
}
