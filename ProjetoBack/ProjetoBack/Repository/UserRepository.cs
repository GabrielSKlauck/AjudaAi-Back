using Dapper;
using Infrastructure;
using ProjetoBack.Infrastructure;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Infrastructure;
using System.Security.Cryptography;
using System.Security.Policy;

namespace Rest.Repository
{

    public class UserRepository : Connection, IUserRepository
    {
        public async Task Add(UserDTO user)
        {
            var Cryptography = new Cryptography(SHA512.Create());       
            string senha = Cryptography.CriptografarSenha(user.Password);

            string sql = @$"INSERT INTO USER (Name, Email, Password, Role, CityId, CityStateId)
                        VALUES(@Name, @Email, '{senha}', @Role, @CityId, @CityStateId)";
            await Execute(sql, user);
        }

        public async Task Delete(int id)
        {
            string sql = "DELETE FROM USER WHERE Id = @id";
            await Execute(sql, new { id });
        }

        public async Task<IEnumerable<UserEntity>> Get()
        {
            string sql = "SELECT * FROM USER";
            return await GetConnection().QueryAsync<UserEntity>(sql);
        }

        public async Task Update(UserEntity user)
        {
            string sql = @"UPDATE USER SET Name = @Name,
                                        Email = @Email,
                                        Password = @Password,
                                        Role = @Role,
                                        CityId = @CityId,
                                        CityStateId = @CityStateId
                                        WHERE Id = @Id";
            await Execute(sql, user);
        }

        public async Task<UserTokenDTO> Login(UserLoginDTO user)
        {
            var Cryptography = new Cryptography(SHA512.Create());
            user.Password = Cryptography.CriptografarSenha(user.Password);

            string sql = "SELECT * FROM USER WHERE Email = @Email AND Password = @Password";
            UserEntity userLogin = await GetConnection().QueryFirstAsync<UserEntity>(sql, user);

            UserAdsRepository.UserId = userLogin.Id;

            return new UserTokenDTO
            {
                Token = Authentication.GenerateTokenUser(userLogin),
                User = userLogin
            };
        }
    }
}