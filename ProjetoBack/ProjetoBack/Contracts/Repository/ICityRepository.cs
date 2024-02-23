using Rest.Entity;

namespace Rest.Contracts.Repository
{
    public interface ICityRepository
    {
        Task<IEnumerable<CityEntity>> Get();

        Task<IEnumerable<CityEntity>> GetByState(int id);

        Task<CityEntity> GetById(int id);

        Task<string> GetCityState(int id);
    }
}
