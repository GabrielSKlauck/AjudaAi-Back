using Dapper;
using ProjetoBack.Contracts.Repository;
using ProjetoBack.DTO;
using ProjetoBack.Entity;
using Rest.Entity;
using Rest.Infrastructure;

namespace ProjetoBack.Repository
{
    public class NgoImagesRepository : Connection, INgoImagesRepository
    {
        public async Task Add(NgoImagesDTO image)
        {
            string sql = "INSERT INTO NGO_IMAGES(Image, NgoId) VALUES (@Image, @NgoId)";
            await Execute(sql, image);
        }

        public async Task Delete(int id)
        {
            string sql = "DELETE FROM NGO_IMAGES WHERE ID = @id";
            await Execute(sql, new {id});
        }

        public async Task<IEnumerable<NgoImagesEntity>> Get(int ngoId)
        {
            string sql = "SELECT * FROM NGO_IMAGES WHERE NGOID = @ngoId";
            return await GetConnection().QueryAsync<NgoImagesEntity>(sql, new { ngoId });
        }

        public async Task Update(NgoImagesEntity image)
        {
            string sql = @"UPDATE NGO_IMAGES SET IMAGE = @IMAGE
                                                        WHERE ID = @ID AND
                                                        NGOID = @NGOID";
            await Execute(sql, image);
        }
    }
}
