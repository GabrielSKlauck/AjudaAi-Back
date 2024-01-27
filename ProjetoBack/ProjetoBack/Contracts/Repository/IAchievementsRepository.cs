using Rest.DTO;
using Rest.Entity;

namespace Rest.Contracts.Repository
{
    public interface IAchievementsRepository
    {
        Task Add(AchievementsDTO entity);

        Task Delete(int id);

        Task<IEnumerable<AchievementsEntity>> GetAll();

        Task<AchievementsEntity> GetById(int id);

        Task Update(AchievementsEntity entity);

        Task UpdateDescription(AchievementsEntity entity);
    }
}
