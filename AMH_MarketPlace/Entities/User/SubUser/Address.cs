using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.User.SubUser
{
    [Table("m_address")]
    public class Address
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "address1", TypeName = "Varchar(255)")] public string? Address1 { get; set; }
        [Column(name: "address2", TypeName = "Varchar(255)")] public string? Address2 { get; set; }
        [Column(name: "city", TypeName = "Varchar(100)")] public string? City { get; set; }
        [Column(name: "post_code", TypeName = "Varchar(5)")] public string? PostCode { get; set; }
        [Column(name: "user_id")] public Guid UserId { get; set; }

    }
}
