using ProjetoBack.DTO;
using ProjetoBack.Entity;

namespace ProjetoBack.Contracts.Repository
{
    public interface INgoImagesRepository
    {
        Task Add(NgoImagesDTO image);

        Task Delete(int id);

        Task Update(NgoImagesEntity image);

        Task<IEnumerable<NgoImagesEntity>> Get(int ngoId);
    }
}
