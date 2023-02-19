using AMH_MarketPlace.Entities.User;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.UserInterface;

namespace AMH_MarketPlace.Services.Implement.UserImplement
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _repository;
        private readonly IDbPersistence _dbPersistence;
        public RoleService(IRepository<Role> repository, IDbPersistence dbPersistence)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
        }

        public async Task<Role> GetOrSaveRole(string role)
        {
            try
            {
                var roleFind = await _repository.Find(r => r.Name.Equals(role));
                if (roleFind != null) return roleFind;

                var saveRole = await _repository.Save(new Role { Name = role });
                await _dbPersistence.SaveChangesAsync();
                return saveRole;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
