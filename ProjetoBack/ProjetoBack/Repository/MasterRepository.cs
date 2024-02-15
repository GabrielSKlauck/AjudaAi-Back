using Dapper;
using Infrastructure;
using ProjetoBack.Infrastructure;
using Rest.Contracts.Repository;
using Rest.DTO;
using Rest.Entity;
using Rest.Infrastructure;
using System.Security.Cryptography;

namespace Rest.Repository
{
    public class MasterRepository : Connection, IMasterRepository
    {
        public async Task<MasterTokenDTO> Login(MasterLoginDTO master)
        {
            var Cryptography = new Cryptography(SHA512.Create());
            master.Password = Cryptography.CriptografarSenha(master.Password);

            string sql = "SELECT * FROM Master WHERE Name = @Name AND Password = @Password";
            MasterEntity masterLogin = await GetConnection().QueryFirstAsync<MasterEntity>(sql, master);

            return new MasterTokenDTO
            {
                Token = Authentication.GenerateTokenMaster(masterLogin),
                Master = masterLogin
            };
        }
    }
}