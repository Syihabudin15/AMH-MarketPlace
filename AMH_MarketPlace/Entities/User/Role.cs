using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.User
{
    [Table("m_role")]
    public class Role
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "name", TypeName = "Varchar(10)")] public string Name { get; set; } = null!;
    }
}
