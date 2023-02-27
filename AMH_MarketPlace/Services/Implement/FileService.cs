using AMH_MarketPlace.Services.Interface;
using System.Net.Http.Headers;
using AMH_MarketPlace.CustomExceptions.ExceptionsHandlers;

namespace AMH_MarketPlace.Services.Implement
{
    public class FileService : IFileService
    {
        public async Task<string> DeleteFile(string path)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);
            File.Delete(fullPath);
            return $"{path} has deleted";
        }

        public async Task<string> SaveFile(IFormFile file, string forWhat)
        {
            var folderName = forWhat == "Store" ? Path.Combine("Resource", "Image", "Store") : Path.Combine("Resource", "Image", "Product");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(pathToSave)) Directory.CreateDirectory(pathToSave);

            if (file.Length <= 0) throw new Exception();

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
            if (fileName == null) throw new NotNullException(new[] { "Name of File is not supported, please update the Name" });

            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);

            await using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return dbPath;
        }

        public async Task<string> UpdateFile(IFormFile file, string pathFile, string forWhat)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), pathFile);
            if (!File.Exists(path))
            {
                var saveFile = await SaveFile(file, forWhat);
                return saveFile;
            }
            File.Delete(path);
            var newFile = await SaveFile(file, forWhat);

            return newFile;
        }
    }
}
