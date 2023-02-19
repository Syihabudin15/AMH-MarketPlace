using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.Store.Product
{
    [Table("m_category_product")]
    public class CategoryProduct
    {
        [Key,Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "name", TypeName = "Varchar(100)")] public string Name { get; set; } = null!;
    }
}
