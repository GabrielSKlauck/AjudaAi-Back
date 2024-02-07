using Dapper;
using ProjetoBack.Entity;
using ProjetoBack.Infrastructure;
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

            

            sql = $"SELECT * FROM USER WHERE {user.UserId}";
            UserEntity userEntity = await GetConnection().QueryFirstAsync<UserEntity>(sql);

            sql = $"SELECT * FROM ADS WHERE Ads.Id = {user.AdsId}";
            AdsEntity ads = await GetConnection().QueryFirstAsync<AdsEntity>(sql);

            sql = $"SELECT * FROM NGO WHERE NGO.id = {ads.Ngo_Id}";
            NGOEntity ngo = await GetConnection().QueryFirstAsync<NGOEntity>(sql);


            string dataFor = DateTime.Now.ToString().Substring(0, 11);

            Console.WriteLine(dataFor);



            Email email = new Email();    
            
            email.SendEmail(new List<string> { $"{ngo.Email}" }, 
               "Um novo voluntario se inscreveu em seu anuncio!", 
               @$"{userEntity.Name} acabou de se inscrever em seu anuncio entitulado de: 
                   {ads.Title}, em {dataFor}. O email para contado de {userEntity.Name}
                    é: {userEntity.Email}. Esse anuncio expira em {ads.Expires}");
        }

        public async Task Delete(int adsId)
        {
            string sql = $"DELETE FROM USER_ADS WHERE UserId = @UserId AND AdsId = @adsId";
            await Execute(sql, new { adsId});
        }

        public async Task Finalizar(int adsId)
        {            
            string sql = "SELECT U.* FROM USER U, USER_ADS A WHERE A.UserId = U.ID AND A.AdsId = @adsId";      
            List<UserEntity> listaUsuarios = (List<UserEntity>)await GetConnection().QueryAsync<UserEntity>(sql, new {adsId});

            List<AchievementsProgressionEntity> listaConquistas = new List<AchievementsProgressionEntity>; 
            
            for (int i = 0; i < listaUsuarios.Count; i++)
            {
                sql = $@"SELECT * FROM Achievements_Progression WHERE user_Id = {listaUsuarios[i].Id}";
                listaConquistas.Add((AchievementsProgressionEntity) await GetConnection().QueryAsync<AchievementsProgressionEntity>(sql));

                for (int j = 0; j < listaConquistas.Count; j++)
                {
                    sql = $@"UPDATE Achievements_Progression SET Score = {listaConquistas[j].Score + 1} WHERE Id = {listaConquistas[j].Id}";
                    await Execute(sql);  
                }
            }
            
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