using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMH_MarketPlace.Entities.Store.Product.Comment
{
    [Table("m_comment_product")]
    public class CommentProduct
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "rate_product")] public int RateProduct { get; set; }
        [Column(name: "description", TypeName = "text")] public string? Description { get; set; }
        [Column(name: "product_id")] public Guid ProductId { get; set; }
    }
}
