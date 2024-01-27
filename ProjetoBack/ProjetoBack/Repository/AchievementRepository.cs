using Dapper;
using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Infrastructure;

namespace Rest.Repository
{
    public class AchievementRepository : Connection, IAchievementsRepository
    {
        public async Task Add(AchievementsDTO entity)
        {
            string sql = "INSERT INTO ACHIEVEMENTS(DESCRIPTION, IMAGE) VALUES(@DESCRIPTION, @IMAGE)";
            await Execute(sql, entity);    
        }

        public async Task Delete(int id)
        {
            string sql = "DELETE FROM ACHIEVEMENTS WHERE ID = @id";
            await Execute(sql, new { id });
        }

        public async Task<IEnumerable<AchievementsEntity>> GetAll()
        {
            string sql = "SELECT * FROM ACHIEVEMENTS";
            return await GetConnection().QueryAsync<AchievementsEntity>(sql);
        }

        public async Task<AchievementsEntity> GetById(int id)
        {
            string sql = "SELECT * FROM ACHIEVEMENTS WHERE ID = @id";
            return await GetConnection().QueryFirstAsync<AchievementsEntity>(sql, new { id });
        }

        public async Task Update(AchievementsEntity entity)
        {
            string sql = @"UPDATE ACHIEVEMENTS SET DESCRIPTION = @DESCRIPTION,
                                            IMAGE = @IMAGE
                                            WHERE ID = @ID";
            await Execute(sql, entity);
        }

        public async Task UpdateDescription(AchievementsEntity entity)
        {
            string sql = @"UPDATE ACHIEVEMENTS SET DESCRIPTION = @DESCRIPTION                                           
                                            WHERE ID = @ID";
            await Execute(sql, entity);
        }
    }
}
