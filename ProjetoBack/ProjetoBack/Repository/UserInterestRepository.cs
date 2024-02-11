using Dapper;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using ProjetoBack.Infrastructure;
using Rest.Contracts.Repository;
using Rest.Controllers;
using Rest.DTO;
using Rest.Entity;
using Rest.Infrastructure;
using System.Security.Cryptography;
using System.Security.Policy;

namespace Rest.Repository
{

    public class UserInterestRepository : Connection, IUserInterestRepository
    {
        public async Task Add(UserInterestDTO user)
        {
            string sql = "INSERT INTO UserInterest (Name, UserId) VALUES (@Name, @UserId)";
            await Execute(sql, user);
        }

        public async Task Delete(int id)
        {
            string sql = "DELETE FROM UserInterest WHERE Id = @id";
            await Execute(sql, new { id});
        }

        public async Task<IEnumerable<UserInterestEntity>> GetByUserId(int id)
        {
            string sql = "SELECT * FROM UserInterest WHERE UserId = @id";
            return await GetConnection().QueryAsync<UserInterestEntity>(sql, new { id });
        }

        public async Task Update(UserInterestEntity user)
        {
            string sql = @"UPDATE UserInterest SET Name = @Name,
                                                    UserId = @UserId
                                                    WHERE Id = @Id";
            await Execute(sql, user);
        }
    }
}