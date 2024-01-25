using Dapper;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Infrastructure;

namespace Rest.Repository
{
    public class UserAdsRepository : Connection, IUserAdsRepository
    {
        public static int UserId { get; set; }
        public async Task Add(UserAdsDTO user)
        {
            string sql = $"INSERT INTO USER_ADS (UserId, AdsId) VALUES (@UserId, @AdsId)";
            await Execute(sql,user);
        }

        public async Task Delete(int adsId)
        {
            string sql = $"DELETE FROM USER_ADS WHERE UserId = @UserId AND AdsId = @adsId";
            await Execute(sql, new { adsId});
        }

        public async Task<IEnumerable<UserAdsEntity>> Get()
        {
            string sql = "SELECT * FROM USER_ADS";
            return await GetConnection().QueryAsync<UserAdsEntity>(sql);
        }

        public async Task<IEnumerable<UserAdsEntity>> GetByUserIdAdsId(UserAdsEntity userAds)
        {
            string sql = "SELECT * FROM USER_ADS WHERE UserId = @UserId AND AdsId = @AdsId";
            return await GetConnection().QueryAsync<UserAdsEntity>(sql, userAds);
        }
    }
}