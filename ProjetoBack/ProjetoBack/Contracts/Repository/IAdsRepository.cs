using Microsoft.AspNetCore.Mvc;
using Rest.DTO;
using Rest.Entity;

namespace Rest.Contracts.Repository
{
    public interface IAdsRepository
    {
        public Task Add(AdsDTO ads);

        public Task<IEnumerable<AdsEntity>> Get();

        public Task Update(AdsEntity ads);

        public Task Delete(int id);
    }
}
