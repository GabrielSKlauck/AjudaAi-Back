using Microsoft.AspNetCore.Mvc;
using Rest.Entity;

namespace Rest.Contracts.Repository
{
    public interface IStateRepository
    {
        Task<IEnumerable<StateEntity>> Get();

        Task<StateEntity> GetById(int ids);
    }
}
