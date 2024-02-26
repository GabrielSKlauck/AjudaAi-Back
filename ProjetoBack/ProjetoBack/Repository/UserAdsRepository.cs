using Dapper;
using ProjetoBack.Entity;
using ProjetoBack.Infrastructure;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Infrastructure;
using System.Globalization;

namespace Rest.Repository
{
    public class UserAdsRepository : Connection, IUserAdsRepository
    {
       
        public async Task Add(UserAdsDTO user)
        {
            string sql = $"INSERT INTO USER_ADS (UserId, AdsId) VALUES (@UserId, @AdsId)";
            await Execute(sql,user);

            

            sql = $"SELECT * FROM USER WHERE {user.UserId}";
            UserEntity userEntity = await GetConnection().QueryFirstAsync<UserEntity>(sql);

            sql = $"SELECT * FROM ADS WHERE Ads.Id = {user.AdsId}";
            AdsEntity ads = await GetConnection().QueryFirstAsync<AdsEntity>(sql);

            sql = $"SELECT * FROM NGO WHERE NGO.id = {ads.Ngo_Id}";
            NGOEntity ngo = await GetConnection().QueryFirstAsync<NGOEntity>(sql);


            string dataFor = DateTime.Now.ToString().Substring(0, 11);
            var dataIncricao = ads.Expires.ToString().Substring(0, 10);
            Console.WriteLine(dataIncricao);

            Email email = new Email();    
            
            email.SendEmail(new List<string> { $"{ngo.Email}" }, 
               "Um novo voluntario se inscreveu em seu anuncio!", 
               @$"{userEntity.Name} acabou de se inscrever em seu anuncio entitulado de: 
                   {ads.Title}, em {dataFor}. O email para contado de {userEntity.Name}
                    é: {userEntity.Email}. Esse anuncio expira em {dataIncricao}");

            email.SendEmail(new List<string> { $"{userEntity.Email}" },
               "Voce se inscreveu em um anuncio",
               @$"Voce decidiu ajuda a ONG {ngo.NgoName}, em seu anuncio entitualdo de {ads.Title} no dia de
                {dataFor}. Obrigado por escolher nossa plataforma! Boa sorte.");


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