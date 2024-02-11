using Microsoft.AspNetCore.Mvc;
using Rest.DTO;
using Rest.Entity;

namespace Rest.Contracts.Repository
{
    public interface IUserInterestRepository
    {
        Task Add(UserInterestDTO user);

        Task Update(UserInterestEntity user);

        Task Delete(int id);

        Task<IEnumerable<UserInterestEntity>> GetByUserId(int id);


    }
}
