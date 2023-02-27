using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.Store.Product
{
    [Table("m_product")]
    public class Product
    {
        [Key,Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "name", TypeName = "Varchar(150)")] public string Name { get; set; } = null!;
        [Column(name: "description", TypeName = "text")] public string Description { get; set; } = null!;
        [Column(name: "weight_kg")] public float? WeightKg { get; set; }
        [Column(name: "weight_g")] public float? WeightG { get; set; }
        [Column(name: "size_inch")] public float? SizeInch { get; set; }
        [Column(name: "size_cm")] public float? SizeCm { get; set; }
        [Column(name: "stock")] public int Stock { get; set; }
        [Column(name: "price")] public long Price { get; set; }
        [Column(name: "category_product_id")] public Guid CategoryProductId { get; set; }
        [Column(name: "store_id")] public Guid StoreId { get; set; }

        public virtual Store? Store { get; set; }
    }
}
