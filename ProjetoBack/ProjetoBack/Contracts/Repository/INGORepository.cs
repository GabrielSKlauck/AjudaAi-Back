using Rest.DTO;
using Rest.Entity;

namespace Rest.Contracts.Repository
{
    public interface INGORepository
    {
        Task Add(NGODTO ngo);

        Task Update(NGOEntity ngo);

        Task <IEnumerable<NGOEntity>> Get();

        Task <NGOEntity> GetById(int id);

        Task<IEnumerable<NGOEntity>> GetByCausesId(int id);

        Task<IEnumerable<NGOEntity>> GetByName(string NgoName);

        Task<NGOEntity> GetByEmail(string NgoName);

        Task Delete(int id);

        Task<NGOTokenDTO> Login(NGOLoginDTO ngo);

        Task SendTokenToEmail(string email);

        Task CheckToken(int token);

        Task ChangePassword(string senha);
    }
}
