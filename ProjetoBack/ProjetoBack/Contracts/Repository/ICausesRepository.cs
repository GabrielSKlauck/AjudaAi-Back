using Rest.Entity;

namespace Rest.Contracts.Repository
{
    public interface ICausesRepository
    {
        Task<IEnumerable<CausesEntity>> Get();

        Task<CausesEntity> GetById(int id);
    }
}
