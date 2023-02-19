using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.User.SubUser.Notifications
{
    [Table("m_notif")]
    public class Notification
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "title", TypeName = "Varchar(255)")] public string? Title { get; set; }
        [Column(name: "body")] public string? Body { get; set; }
        [Column(name: "created_at")] public DateTime CreatedAt { get; set; }
        [Column(name: "user_id")] public Guid? UserId { get; set; }
        [Column(name: "is_read_id")] public Guid NotifReadId { get; set; }
        [Column(name: "category_notification_id")] public Guid CategoryNotificationId { get; set; }

        public virtual CategoryNotification? CategoryNotification { get; set; }
        public virtual NotifRead? NotifRead { get; set; }
    }
}
