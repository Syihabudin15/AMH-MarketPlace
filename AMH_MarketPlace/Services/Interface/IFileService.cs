
namespace AMH_MarketPlace.Services.Interface
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file, string forWhat);
        Task<string> UpdateFile(IFormFile file, string pathFile, string forWhat);
        Task<string> DeleteFile(string path);
    }
}
