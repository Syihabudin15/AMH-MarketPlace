using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.Store
{
    [Table("m_rate_store")]
    public class RateStore
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "rate1")] public int Rate1 { get; set; }
        [Column(name: "rate2")] public int Rate2 { get; set; }
        [Column(name: "rate3")] public int Rate3 { get; set; }
        [Column(name: "rate4")] public int Rate4 { get; set; }
        [Column(name: "rate5")] public int Rate5 { get; set; }

    }
}
