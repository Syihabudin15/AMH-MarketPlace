using AMH_MarketPlace.Entities.Store;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Services.Interface.StoreInterface;
using AMH_MarketPlace.CustomExceptions.ExceptionsHandlers;

namespace AMH_MarketPlace.Services.Implement.StoreImplement
{
    public class RateService : IRateService
    {
        private readonly IRepository<RateStore> _repository;
        private readonly IDbPersistence _dbPersistence;
        public RateService(IRepository<RateStore> repository, IDbPersistence dbPersistence)
        {
            _repository = repository;
            _dbPersistence = dbPersistence;
        }

        public async Task<RateStore> CreateRateStore(RateStore rateStore)
        {
            var rate = await _repository.Save(rateStore);
            await _dbPersistence.SaveChangesAsync();
            return rate;
        }

        public async Task<RateStore> UpdateRateStore(int numberRate, string id)
        {

            var rateFind = await _repository.Find(r => r.Id.Equals(Guid.Parse(id)));
            if (rateFind == null) throw new NotFoundException("Rate not Found");

            switch (numberRate)
            {
                case 1:
                    rateFind.Rate1++;
                    break;
                case 2:
                    rateFind.Rate2 ++;
                    break;
                case 3:
                    rateFind.Rate3++;
                    break;
                case 4:
                    rateFind.Rate4++;
                    break;
                case 5:
                    rateFind.Rate5++;
                    break;
                default:
                    throw new NotNullException(new[] { "Please input a valid Number Rate : 1/2/3/4/5" });
            }

            var updateRate = _repository.Update(rateFind);
            await _dbPersistence.SaveChangesAsync();
            return updateRate;
        }
    }
}
