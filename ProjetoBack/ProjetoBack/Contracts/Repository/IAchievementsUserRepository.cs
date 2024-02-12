using Rest.DTO;
using Rest.Entity;

namespace Rest.Contracts.Repository
{
    public interface IAchievementsUserRepository
    {
        Task Add(AchievementsUserDTO dto);

        Task Delete(AchievementsUserDTO dto);

        Task<IEnumerable<AchievementsEntity>> GetAchievementsCompletedByUserId(int id);

        Task<IEnumerable<AchievementsEntity>> GetAchievementsIncompleteByUserId(int id);

        Task<AchievementsUserDTO> GetConquest(int userId, int achieId);
    
    }
}