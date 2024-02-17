using Dapper;
using Rest.Contracts.Repository;
using Rest.Entity;
using Rest.Infrastructure;

namespace Rest.Repository
{
    public class CausesRepository : Connection, ICausesRepository
    {
        public async Task<IEnumerable<CausesEntity>> Get()
        {
            string sql = "SELECT * FROM CAUSES";
            return await GetConnection().QueryAsync<CausesEntity>(sql);
        }

        public async Task<CausesEntity> GetById(int id)
        {
            string sql = "SELECT * FROM CAUSES WHERE Id = @Id";
            return await GetConnection().QueryFirstAsync<CausesEntity>(sql, new {id});
        }
    }
}
