using Dapper;
using Microsoft.AspNetCore.Mvc;
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
    }
}
