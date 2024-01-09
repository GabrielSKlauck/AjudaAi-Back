using Microsoft.AspNetCore.Mvc;
using Rest.DTO;
using Rest.Entity;

namespace Rest.Contracts.Repository
{
    public interface IUserRepository
    {
        Task Add(UserDTO user);

        Task<IEnumerable<UserEntity>> Get();
     
        Task Update(UserEntity user);

        Task Delete(int id);

        Task<UserTokenDTO> Login(UserLoginDTO user);
    }
}
