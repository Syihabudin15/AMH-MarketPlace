using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.User
{
    [Table("m_credential")]
    public class Credential
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }
        [EmailAddress, Column(name: "email", TypeName = "Varchar(100)")] public string Email { get; set; } = null!;
        [Column(name: "password"), StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
        public string Password { get; set; } = null!;
        [Column(name: "role_id")] public Guid RoleId { get; set; }

        public virtual Role? Role { get; set; }
    }
}
