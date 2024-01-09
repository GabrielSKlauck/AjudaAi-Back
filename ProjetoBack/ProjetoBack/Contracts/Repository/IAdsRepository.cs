using Microsoft.AspNetCore.Mvc;
using Rest.DTO;
using Rest.Entity;

namespace Rest.Contracts.Repository
{
    public interface IAdsRepository
    {
        Task Add(AdsDTO ads);

        Task<IEnumerable<AdsEntity>> Get();

        Task<AdsEntity> GetById(int id);

        Task<IEnumerable<AdsEntity>> GetByCausesId(int id);

        Task Update(AdsEntity ads);

        Task Delete(int id);

        Task<IEnumerable<AdsEntity>> GetByNgoId(int id);
    }
}
