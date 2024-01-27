using Dapper;
using Infrastructure;
using ProjetoBack.Infrastructure;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Infrastructure;
using System.Security.Cryptography;
using System.Security.Policy;

namespace Rest.Repository
{

    public class UserImageRepository : Connection, IUserImageRepository
    {
        public async Task Add(UserImageDTO user)
        {
            string sql = "INSERT INTO USER_PROFILE_IMAGE(IMAGE, USERID) VALUES(@IMAGE, @USERID)";
            await Execute(sql, user);
        }

        public async Task Delete(int id)
        {
            string sql = "DELETE FROM USER_PROFILE_IMAGE WHERE ID = @id";
            await Execute(sql, new { id }); 
        }

        public async Task<UserImageEntity> GetByUserId(int id)
        {
            string sql = "SELECT * FROM USER_PROFILE_IMAGE WHERE ID = @id";
            return await GetConnection().QueryFirstAsync<UserImageEntity>(sql, new { id });
        }

        public async Task Update(UserImageEntity user)
        {
            string sql = "UPDATE USER_PROFILE_IMAGE SET IMAGE = @IMAGE WHERE ID = @ID";
            await Execute(sql, user);
        }
    }
}