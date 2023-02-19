using AMH_MarketPlace.Entities.Store;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface;
using AMH_MarketPlace.Services.Interface.StoreInterface;
using AMH_MarketPlace.CustomExceptions.ExceptionsHandlers;

namespace AMH_MarketPlace.Services.Implement.StoreImplement
{
    public class StoreImageService : IStoreImageService
    {
        private readonly IRepository<StoreImage> _repository;
        private readonly IDbPersistence _dbPersistence;
        private readonly IFileService _fileService;
        public StoreImageService(
            IRepository<StoreImage> repository,
            IDbPersistence dbPersistence,
            IFileService fileService)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
            _fileService = fileService;
        }

        public async Task<StoreImage> SaveStoreImage(IFormFile? file)
        {

            if (file == null)
            {
                var saveDefault = await _repository.Save(new StoreImage
                {
                    FileName = "isDefault",
                    FilePath = "isDefault",
                    FileSize = 0,
                    ContenType = "isDefault"
                });
                await _dbPersistence.SaveChangesAsync();
                return saveDefault;
            }
            var filePath = await _fileService.SaveFile(file, "Store");
            var storeImage = new StoreImage
            {
                FileName = file.FileName,
                FileSize = file.Length,
                FilePath = filePath,
                ContenType = file.ContentType
            };
            var saveStoreImage = await _repository.Save(storeImage);
            await _dbPersistence.SaveChangesAsync();
            return saveStoreImage;

        }

        public async Task<StoreImage> UpdateStoreImage(IFormFile? file, Guid id)
        {

            var imageFind = await _repository.Find(i => i.Id.Equals(id));
            if (imageFind == null) throw new NotFoundException("Store image not found");
            if (file == null) return imageFind;

            var updateOrSave = await _fileService.UpdateFile(file, imageFind.FilePath, "Store");

            var updateStoreImage = _repository.Update(new StoreImage
            {
                Id = imageFind.Id,
                FileName = file.Name,
                FileSize = file.Length,
                FilePath = updateOrSave,
                ContenType = file.ContentType
            });

            await _dbPersistence.SaveChangesAsync();
            return updateStoreImage;

        }
    }
}
