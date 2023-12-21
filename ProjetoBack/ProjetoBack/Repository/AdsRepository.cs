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
            string sql = @"INSERT INTO ADS (Title, Description, Expires, Ngo_Id)
                           VALUES(@Title, @Description, @Expires, NGO_Id)";
            await Execute(sql, ads);
               
        }

        public async Task Delete(int id)
        {
            string sql = "DELETE FROM ADS WHERE Id = id AND Ngo_Id = NGO_Id";
            await Execute(sql, new { id });
        }

        public Task<IEnumerable<AdsEntity>> Get()
        {
            throw new NotImplementedException();
        }

        public Task Update(AdsEntity ads)
        {
            throw new NotImplementedException();
        }
    }
}
