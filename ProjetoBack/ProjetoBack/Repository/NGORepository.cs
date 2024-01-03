using Dapper;
using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.Infrastructure;
using Rest.DTO;
using Rest.Entity;
using Infrastructure;

namespace Rest.Repository
{
    public class NGORepository : Connection, INGORepository
    {

        public async Task Add(NGODTO ngo)
        {
            string sql = @"INSERT INTO NGO(NgoName, Description, Site, HeadPerson, Telephone, Email, Password, Role, CityId, CausesId)
                            VALUES(@NgoName, @Description, @Site, @HeadPerson, @Telephone, @Email, @Password, @Role, @CityId, @CausesId)";
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

        public async Task<NGOEntity> GetById(int id)
        {
            string sql = "SELECT * FROM NGO WHERE Id = @id";
            return await GetConnection().QueryFirstAsync<NGOEntity>(sql, new { id });
        }

        public async Task<NGOTokenDTO> Login(NGOLoginDTO ngo)
        {
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
                                          CityId = @CityId, 
                                          CausesId = @CausesId
                                          WHERE Id = @Id";
            await Execute(sql, ngo);
        }
       
    }
}

