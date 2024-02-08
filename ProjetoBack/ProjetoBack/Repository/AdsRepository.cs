using Dapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoBack.Entity;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Infrastructure;

namespace Rest.Repository
{
    public class AdsRepository : Connection, IAdsRepository
    {      
        public static int NGO_Id { get; set; }
        public async Task Add(AdsDTO ads)
        {
            string sql = @$"INSERT INTO ADS (Title, Description, Expires, Ngo_Id)
                           VALUES(@Title, @Description, @Expires, {NGO_Id})";
            await Execute(sql, ads);
               
        }

        public async Task Delete(int id)
        {
            string sql = "DELETE FROM ADS WHERE Id = @id";
            await Execute(sql, new { id });
        }

        public async Task<IEnumerable<AdsEntity>> Get()
        {
            string sql = "SELECT * FROM ADS";
            return await GetConnection().QueryAsync<AdsEntity>(sql);
        }
     
        public async Task<IEnumerable<AdsEntity>> GetByCausesId(int id)
        {
            string sql = "SELECT A.* FROM ADS A, NGO N WHERE N.CausesId = @id AND N.id = A.ngo_id";
            return await GetConnection().QueryAsync<AdsEntity>(sql, new {id});    
        }

        public async Task Update(AdsEntity ads)
        {
            string sql = @$"UPDATE ADS SET 
                            Title = @Title,
                            Description = @Description,
                            Expires = @Expires,
                            Ngo_Id = {NGO_Id}
                            WHERE Id = @Id";

             await Execute(sql, ads);
        }

        public async Task<AdsEntity> GetById(int id)
        {
            string sql = "SELECT * FROM ADS WHERE Id = @id";
            return await GetConnection().QueryFirstAsync<AdsEntity>(sql, new { id });
        }
        public async Task<IEnumerable<AdsEntity>> GetByNgoId(int id)
        {
            string sql = "SELECT * FROM ADS WHERE NGO_ID = @id";
            return await GetConnection().QueryAsync<AdsEntity>(sql, new {id});
        }

        public async Task Finalizar(int adsId)
        {
            string sql = "SELECT U.* FROM USER U, USER_ADS A WHERE A.UserId = U.ID AND A.AdsId = @adsId";
            List<UserEntity> listaUsuarios = (List<UserEntity>)await GetConnection().QueryAsync<UserEntity>(sql, new { adsId });



            for (int i = 0; i < listaUsuarios.Count; i++)
            {

                sql = $@"SELECT * FROM Achievements_Progression WHERE user_Id = {listaUsuarios[i].Id}";

                List<AchievementsProgressionEntity> listaConquistas = (List<AchievementsProgressionEntity>)await GetConnection().QueryAsync<AchievementsProgressionEntity>(sql);

                //TESTE COMPLETAMENTE O METODO

                int qtdRequerida;

                for (int j = 0; j < listaConquistas.Count; j++)
                {
                    listaConquistas[j].Score++;
                    qtdRequerida = Int32.Parse(listaConquistas[j].Acronym.Replace("C", "").Replace("T", ""));

                    if (listaConquistas[j].Score == qtdRequerida)
                    {

                        sql = $"SELECT Id FROM achievements WHERE Acronym LIKE '{listaConquistas[j].Acronym}'";
                        int idConquista = await GetConnection().QueryFirstAsync<Int32>(sql);

                        sql = $"INSERT INTO achievements_user(AchievementsId, UserId) VALUES({idConquista},{listaUsuarios[i].Id})";
                        await ExecuteOnly(sql);

                        sql = $"DELETE FROM Achievements_Progression WHERE id = {listaConquistas[j].Id}";
                        await ExecuteOnly(sql);

                    }
                    else
                    {
                        sql = $"UPDATE Achievements_Progression SET Score = {listaConquistas[j].Score} WHERE Id = {listaConquistas[j].Id}";
                        await ExecuteOnly(sql);
                    }

                }

                sql = "SELECT N.CausesId FROM NGO N, ADS A WHERE A.Id = @adsId AND A.Ngo_Id = N.Id";
                int causaId = await GetConnection().QueryFirstAsync<int>(sql, new { adsId });

                sql = $"SELECT Acronym FROM CAUSES WHERE Id = {causaId}";
                string acronym = await GetConnection().QueryFirstAsync<string>(sql);

                sql = $"Select Id FROM achievements WHERE Acronym LIKE '{acronym}'";
                int conquistaId = await GetConnection().QueryFirstAsync<int>(sql);

                try
                {
                    sql = $"INSERT INTO Achievements_User(AchievementsId, UserId) VALUES({conquistaId}, {listaUsuarios[i].Id})";
                    await ExecuteOnly(sql);
                }
                catch (Exception ex) { }

            }

            sql = $"DELETE FROM User_Ads WHERE AdsId = @adsId";
            await Execute(sql, new { adsId });

            sql = $"DELETE FROM Ads WHERE Id = @adsId";
            await Execute(sql, new { adsId });

        }
    }
}
