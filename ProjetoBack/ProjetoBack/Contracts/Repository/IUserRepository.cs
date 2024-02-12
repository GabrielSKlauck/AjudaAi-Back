using Microsoft.AspNetCore.Mvc;
using Rest.DTO;
using Rest.Entity;

namespace Rest.Contracts.Repository
{
    public interface IUserRepository
    {
        Task Add(UserDTO user);

        Task<IEnumerable<UserEntity>> Get();

        Task<UserEntity> GetById(int id);

        Task<UserEntity> GetByEmail(string email);

        Task<string> GetImageById(int id);
     
        Task Update(UserEntity user);

        Task UpdateProfileImage(UserImageUpdateEntity user);

        Task ShortUpdate(UserUpdateEntity user);

        Task Delete(int id);   

        Task<UserTokenDTO> Login(UserLoginDTO user);

        Task SendTokenToEmail(string email);

        Task CheckToken(int token);

        Task ChangePassword(string senha);
    }
}
