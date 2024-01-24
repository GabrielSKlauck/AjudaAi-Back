using Dapper;
using ProjetoBack.Contracts.Repository;
using ProjetoBack.DTO;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Infrastructure;

namespace ProjetoBack.Repository
{
    public class UserCausesRepository : Connection, IUserCausesRepository
    {
        public async Task Add(UserCausesDTO user)
        {
            string sql = "INSERT INTO USER_CAUSES(UserId, CausesId) VALUES (@UserId, @CausesId)";
            await Execute(sql, user);
        }

        public async Task Delete(UserCausesDTO user)
        {
            string sql = "DELETE FROM USER_CAUSES WHERE UserId = @UserId AND CausesId = @CausesId";
            await Execute(sql, user);
        }

        public async Task<IEnumerable<CausesEntity>> GetUserCausesById(int id)
        {
            string sql = "select ID, Name from user_causes uc, causes c where uc.userId = @id and uc.CausesId = c.id";
            return await GetConnection().QueryAsync<CausesEntity>(sql, new {id});
        }

        public async Task Update(UserCausesDTO user)
        {
            string sql = "DELETE FROM USER_CAUSES WHERE UserId = @UserId AND CausesId = @CausesId";
            await Execute(sql, user);
            string sql = 
        }
    }
}
