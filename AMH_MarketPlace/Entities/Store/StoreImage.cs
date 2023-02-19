using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AMH_MarketPlace.Entities.Store
{
    [Table("m_store_image")]
    public class StoreImage
    {
        [Key, Column(name: "id")] public Guid Id { get; set; }
        [Column(name: "file_name")] public string FileName { get; set; } = null!;
        [Column(name: "file_size")] public long FileSize { get; set; }
        [Column(name: "file_path")] public string FilePath { get; set; } = null!;
        [Column(name: "conten_type")] public string ContenType { get; set; } = null!;
    }
}
