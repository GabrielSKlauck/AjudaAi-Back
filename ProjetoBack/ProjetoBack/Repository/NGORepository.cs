using Dapper;
using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.Infrastructure;
using Rest.DTO;
using Rest.Entity;
using Infrastructure;
using System.Security.Cryptography;
using ProjetoBack.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Rest.Controllers;
using ProjetoBack.Entity;

namespace Rest.Repository
{
    public class NGORepository : Connection, INGORepository
    {
        private static int savedToken;
        private static int idNgo;

        public async Task Add(NGODTO ngo)
        {
            var Cryptography = new Cryptography(SHA512.Create());
            string senha = Cryptography.CriptografarSenha(ngo.Password);

            string sql = @$"INSERT INTO NGO(NgoName, Description, Site, HeadPerson, Telephone, Email, Password, Role, CausesId, CityId, CityStateId)
                            VALUES(@NgoName, @Description, @Site, @HeadPerson, @Telephone, @Email, '{senha}', @Role, @CausesId, @CityId, @CityStateId)";
            await Execute(sql, ngo);
        }

        public async Task Delete(int id)
        {
            string sql = "DELETE FROM NGO WHERE Id = @id";
            await Execute(sql, new { id });
        }

        public async Task<IEnumerable<NGOEntity>> Get()
        {
            string sql = "SELECT * FROM NGO";
            return await GetConnection().QueryAsync<NGOEntity>(sql);
        }

        public async Task<IEnumerable<NGOEntity>> GetByCausesId(int id)
        {
            string sql = "SELECT * FROM NGO WHERE CAUSESID = @id";
            return await GetConnection().QueryAsync<NGOEntity>(sql, new { id });
        }

        public async Task<NGOEntity> GetById(int id)
        {
            string sql = "SELECT * FROM NGO WHERE Id = @id";
            return await GetConnection().QueryFirstAsync<NGOEntity>(sql, new { id });
        }

        public async Task<IEnumerable<NGOEntity>> GetByName(string NgoName)
        {
            string sql = $"SELECT * FROM NGO WHERE NGONAME LIKE '%{NgoName}%'";
            return await GetConnection().QueryAsync<NGOEntity>(sql, new { NgoName });
        }

        public async Task<NGOEntity> GetByEmail(string email)
        {
            string sql = $"SELECT * FROM NGO WHERE email LIKE '{email}'";
            return await GetConnection().QueryFirstAsync<NGOEntity>(sql, new { email });
        }

        public async Task<NGOTokenDTO> Login(NGOLoginDTO ngo)
        {
            var Cryptography = new Cryptography(SHA512.Create());
            ngo.Password = Cryptography.CriptografarSenha(ngo.Password);

            string sql = "SELECT * FROM NGO WHERE Email = @Email AND Password = @Password";
            NGOEntity ngoLogin = await GetConnection().QueryFirstAsync<NGOEntity>(sql, ngo);

            return new NGOTokenDTO
            {
                Token = Authentication.GenerateToken(ngoLogin),
                User = ngoLogin
            };
        }
        public async Task Update(NGOEntity ngo)
        {
            string sql = @$"UPDATE NGO SET NgoName = @NgoName,
                                          Description = @Description,
                                          Site = @Site, 
                                          HeadPerson = @HeadPerson, 
                                          Telephone = @Telephone, 
                                          Email = @Email, 
                                          Password = @Password, 
                                          Role = @Role,                                           
                                          CausesId = @CausesId,
                                          CityId = @CityId,
                                          CityStateId = @CityStateId
                                          WHERE Id = @Id";
            await Execute(sql, ngo);
        }

        public async Task UpdatePerfil(NgoUpdateEntity ngo)
        {
            string sql = @"UPDATE NGO SET NgoName = @NgoName,
                                          Description = @Description,
                                          CausesId = @CausesId,
                                          CityId = @CityId,
                                          CityStateId = @CityStateId
                                          WHERE Id = @Id";
            await Execute(sql, ngo);
        }

        public async Task SendTokenToEmail(string email)
        {
            if (await VerificarEmailBanco(email))
            {
                Random rand = new Random();
                savedToken = rand.Next(10000);
                Email emailObj = new Email();
                emailObj.SendEmail(new List<string> { email }, "Token de verificação de email", $"Aqui esta o token de validação.\n\n{savedToken}. Se " +
                    $"Voce não requisitou token de autentificação, pode ignorar este email.");
            }
        }

        public async Task CheckToken(int token)
        {
            if (token == savedToken)
            {
                NGOController.tokenValidado = true;
            }
            else
            {
                NGOController.tokenValidado = false;
            }
        }

        public async Task ChangePassword(string senha)
        {
            var Cryptography = new Cryptography(SHA512.Create());

            senha = Cryptography.CriptografarSenha(senha);
            string sql = $"UPDATE NGO SET PASSWORD = @senha WHERE ID = {idNgo}";
            await Execute(sql, new { senha });
        }

        private async Task<bool> VerificarEmailBanco(string email)
        {
            string sql = "SELECT * FROM NGO WHERE Email = @email";
            NGOEntity ngo = await GetConnection().QueryFirstAsync<NGOEntity>(sql, new { email });
            idNgo = ngo.Id;

            if (ngo != null)
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

