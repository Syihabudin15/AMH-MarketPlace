using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AMH_MarketPlace.Entities.Store.Product
{
    [Table("m_product_image")]
    public class ProductImage
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "file_name")] public string FileName { get; set; } = null!;
        [Column(name: "file_size")] public long FileSize { get; set; }
        [Column(name: "file_path")] public string FilePath { get; set; } = null!;
        [Column(name: "conten_type")] public string ContenType { get; set; } = null!;
        [Column(name: "product_id")] public Guid ProductId { get; set; }
    }
}
