using Dapper;
using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.Entity;
using Rest.Infrastructure;

namespace Rest.Repository
{
    public class CityRepository : Connection, ICityRepository
    {
        public async Task<IEnumerable<CityEntity>> Get()
        {
            string sql = "SELECT * FROM CITY";
            return await GetConnection().QueryAsync<CityEntity> (sql);
        }

        public async Task<IEnumerable<CityEntity>> GetByState(int id)
        {
            string sql = "SELECT * FROM CITY WHERE State_Id = @id";
            return await GetConnection().QueryAsync<CityEntity>(sql, new {id});
        }

        public async Task<CityEntity> GetById(int id)
        {
            string sql = "SELECT * FROM CITY WHERE Id = @id";
            return await GetConnection().QueryFirstAsync<CityEntity>(sql, new { id });
        }

        public async Task<string> GetCityState(int id)
        {
            string localizacao;

            string sql = "SELECT * FROM CITY WHERE Id = @id";
            CityEntity cidade = await GetConnection().QueryFirstAsync<CityEntity>(sql, new { id });

            localizacao = cidade.Name + ", ";

            sql = $"SELECT * FROM STATE WHERE Id = {cidade.State_Id}";
            StateEntity state = await GetConnection().QueryFirstAsync<StateEntity>(sql, new { id });

            localizacao += state.Name;
            return localizacao;

        }

        
    }
}
