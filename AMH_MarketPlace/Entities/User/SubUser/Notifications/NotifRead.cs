using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.User.SubUser.Notifications
{
    [Table("m_notif_read")]
    public class NotifRead
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "is_read")] public bool IsRead { get; set; }
    }
}
