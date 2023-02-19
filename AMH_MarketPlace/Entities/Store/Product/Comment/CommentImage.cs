using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.Store.Product.Comment
{
    [Table("m_comment_image")]
    public class CommentImage
    {
        [Key,Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "file_name")] public string FileName { get; set; } = null!;
        [Column(name: "file_size")] public long FileSize { get; set; }
        [Column(name: "file_path")] public string FilePath { get; set; } = null!;
        [Column(name: "conten_type")] public string ContenType { get; set; } = null!;
        [Column(name: "comment_product_id")] public Guid CommentProductId { get; set; }
    }
}
