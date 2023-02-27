using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AMH_MarketPlace.Entities.Store.Product;

namespace AMH_MarketPlace.Entities.Store
{
    [Table("m_store")]
    public class Store
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "name", TypeName = "Varchar(150)")] public string Name { get; set; } = null!;
        [Column(name: "description", TypeName = "text")] public string Description { get; set; } = null!;
        [Column(name: "store_image_id")] public Guid StoreImageId { get; set; }
        [Column(name: "rate_store_id")] public Guid RateStoreId { get; set; }
        [Column(name: "user_id")] public Guid UserId { get; set; }

        public virtual StoreImage? StoreImage { get; set; }
        public virtual RateStore? RateStore { get; set; }
    }
}
