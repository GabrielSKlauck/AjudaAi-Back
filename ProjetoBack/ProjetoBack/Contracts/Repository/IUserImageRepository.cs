using Microsoft.AspNetCore.Mvc;
using Rest.DTO;
using Rest.Entity;

namespace Rest.Contracts.Repository
{
    public interface IUserImageRepository
    {
        Task Add(UserImageDTO user);

        Task<UserImageEntity> GetByUserId(int id);

        Task Update(UserImageEntity user);

        Task Delete(int id);
     
    }
}
