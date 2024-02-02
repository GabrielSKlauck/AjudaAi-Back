using Dapper;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ProjetoBack.Infrastructure;
using Rest.Contracts.Repository;
using Rest.Controllers;
using Rest.DTO;
using Rest.Entity;
using Rest.Infrastructure;
using System.Security.Cryptography;
using System.Security.Policy;

namespace Rest.Repository
{

    public class UserRepository : Connection, IUserRepository
    {
        private static int savedToken;
        private static int idUser;

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

        public async Task SendTokenToEmail(string email)
        {
            

            if (await VerificarEmailBanco(email))
            {
                Random rand = new Random();
                savedToken = rand.Next(10000);
                Email emailObj = new Email();
                emailObj.SendEmail(new List<string> { email }, "Token de verificação de email", $"Aqui esta o token de validação.\n\n{savedToken}");
            }
        }

        public async Task CheckToken(int token)
        {
            if (token == savedToken)
            {               
                UserController.tokenValidado = true;
            }
            else
            {
                UserController.tokenValidado = false;
            }
        }

        public async Task ChangePassword(string senha)
        {
            var Cryptography = new Cryptography(SHA512.Create());

            senha = Cryptography.CriptografarSenha(senha);
            string sql = $"UPDATE USER SET PASSWORD = @senha WHERE ID = {idUser}";
            await Execute(sql, new {senha});
        }

        private async Task<bool> VerificarEmailBanco(string email)
        {
            string sql = "SELECT * FROM USER WHERE Email = @email";
            UserEntity user =  await GetConnection().QueryFirstAsync<UserEntity>(sql, new {email});
            idUser = user.Id;

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}