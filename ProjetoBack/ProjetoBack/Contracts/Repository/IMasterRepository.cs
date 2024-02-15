using Rest.DTO;

namespace Rest.Contracts.Repository
{
    public interface IMasterRepository
    {
        Task<MasterTokenDTO> Login(MasterLoginDTO master);
    }
}