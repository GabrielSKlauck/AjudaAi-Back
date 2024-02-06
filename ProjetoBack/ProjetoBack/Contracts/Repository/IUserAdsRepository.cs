using Rest.DTO;
using Rest.Entity;

namespace Rest.Contracts.Repository
{
    public interface IUserAdsRepository
    {
        Task Add(UserAdsDTO user);

        Task<IEnumerable<UserAdsEntity>> Get();      
        
        Task Delete(int AdsId);

        Task<IEnumerable<UserAdsEntity>> GetByUserIdAdsId(UserAdsEntity userAds);

        Task Finalizar(int adsId);
    }
}