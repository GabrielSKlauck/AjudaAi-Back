using Dapper;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Infrastructure;

namespace Rest.Repository;

public class UserRepository : Connection, IUserRepository
{
    public async Task Add(UserDTO user)
    {
        string sql = @"INSERT INTO USER (Name, Email, Password, CityId)
                        VALUES(@Name, @Email, @Password, @CityId)";
        await Execute(sql, user);
    }

    public async Task Delete(int id)
    {
        string sql = "DELETE FROM USER WHERE Id = @id";
        await Execute(sql, new { id });
    }

    public async Task<IEnumerable<UserEntity>> Get()
    {
        string sql = "SELECT * FROM USER";
        return await GetConnection().QueryAsync<UserEntity>(sql);
    }
  
    public async Task Update(UserEntity user)
    {
        string sql = @"UPDATE USER SET Name = @Name,
                                        Email = @Email,
                                        Password = @Password,
                                        CityId = @CityId
                                        WHERE Id = @Id";
        await Execute(sql, user);
    }
}