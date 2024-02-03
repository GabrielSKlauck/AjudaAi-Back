using Dapper;
using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.Infrastructure;
using Rest.DTO;
using Rest.Entity;
using Infrastructure;
using System.Security.Cryptography;
using ProjetoBack.Infrastructure;

namespace Rest.Repository
{
    public class NGORepository : Connection, INGORepository
    {

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

        public async Task<NGOTokenDTO> Login(NGOLoginDTO ngo)
        {
            var Cryptography = new Cryptography(SHA512.Create());
            ngo.Password = Cryptography.CriptografarSenha(ngo.Password);

            string sql = "SELECT * FROM NGO WHERE Email = @Email AND Password = @Password";
            NGOEntity ngoLogin = await GetConnection().QueryFirstAsync<NGOEntity>(sql, ngo);

            AdsRepository.NGO_Id = ngoLogin.Id;

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
       
    }
}

