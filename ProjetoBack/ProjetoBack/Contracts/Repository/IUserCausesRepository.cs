using ProjetoBack.DTO;
using Rest.DTO;
using Rest.Entity;

namespace ProjetoBack.Contracts.Repository
{
    public interface IUserCausesRepository
    {
        Task Add(UserCausesDTO user);

       // Task Update(UserCausesDTO user);

        Task<IEnumerable<CausesEntity>> GetUserCausesById(int id);

        Task Delete(UserCausesDTO users);
     
    }
}
