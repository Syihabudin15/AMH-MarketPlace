using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.User.SubUser.Notifications
{
    [Table("m_category_notif")]
    public class CategoryNotification
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "name", TypeName = "Varchar(10)")] public string Name { get; set; } = null!;
    }
}
