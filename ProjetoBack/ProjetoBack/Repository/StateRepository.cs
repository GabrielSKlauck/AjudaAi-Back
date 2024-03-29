﻿using Dapper;
using Rest.Contracts.Repository;
using Rest.Entity;
using Rest.Infrastructure;

namespace Rest.Repository
{
    public class StateRepository : Connection, IStateRepository
    {
        public async Task<IEnumerable<StateEntity>> Get()
        {
            string sql = "SELECT * FROM STATE";
            return await GetConnection().QueryAsync<StateEntity>(sql);
        }

        public async Task<StateEntity> GetById(int id)
        {
            string sql = "SELECT * FROM STATE WHERE Id = @id";
            return await GetConnection().QueryFirstAsync<StateEntity>(sql, new {id });
        }
    }
}
