using AMH_MarketPlace.Entities.User.SubUser;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.User
{
    [Table("m_user")]
    public class User
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "first_name", TypeName = "Varchar(50)")] public string FirstName { get; set; } = null!;
        [Column(name: "last_name", TypeName = "Varchar(50)")] public string LastName { get; set; } = null!;
        [Column(name: "phone_number", TypeName = "Varchar(13)")] public string PhoneNumber { get; set; } = null!;
        [Column(name: "credential_id")] public Guid CredentialId { get; set; }

        public virtual Credential? Credential { get; set; }
        public virtual Address? Address { get; set; }
    }
}
