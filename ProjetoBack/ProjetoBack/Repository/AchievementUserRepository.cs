using Dapper;
using Microsoft.AspNetCore.Mvc;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Infrastructure;

namespace Rest.Repository
{
    public class AchievementUserRepository : Connection, IAchievementsUserRepository
    {

        public async Task Add(AchievementsUserDTO dto)
        {
            string sql = $@"INSERT INTO ACHIEVEMENTS_USER(AchievementsId, UserId, CompletionDate)
                            VALUES(@AchievementsId, @UserId, current_date())";
            await Execute(sql, dto);
        }

        public async Task Delete(AchievementsUserDTO dto)
        {
            string sql = @"DELETE FROM ACHIEVEMENTS_USER WHERE 
                AchievementsId = @AchievementsId AND UserId = @UserId";
            await Execute(sql, dto);
        }

        public async Task<IEnumerable<AchievementsEntity>> GetAchievementsCompletedByUserId(int id)
        {
            string sql = @"select a.* from user u, achievements a, achievements_user au 
                        where @id = au.userid and au.AchievementsId = a.id group by a.id";
            return await GetConnection().QueryAsync<AchievementsEntity>(sql, new { id });
        }

        public async Task<IEnumerable<AchievementsEntity>> GetAchievementsIncompleteByUserId(int id)
        {
            string sql = @"SELECT a.* FROM achievements a WHERE NOT EXISTS (SELECT 1
                                                                            FROM achievements_user au
                                                                            WHERE au.AchievementsId = a.id
                                                                            AND au.userid = @id)";
            return await GetConnection().QueryAsync<AchievementsEntity>(sql, new { id });
        }    
    }
}
