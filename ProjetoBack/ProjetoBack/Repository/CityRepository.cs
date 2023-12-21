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
    }
}
