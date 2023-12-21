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
    }
}
