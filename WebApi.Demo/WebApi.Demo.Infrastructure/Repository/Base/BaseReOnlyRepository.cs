using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Demo.Domain;

namespace WebApi.Demo.Infrastructure
{
    public abstract class BaseReOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        protected readonly string _connectionString;
        protected virtual string TableName { get; set; } = typeof(TEntity).Name;

        protected BaseReOnlyRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<TEntity?> FindAsync(TKey id)
        {
            var connection = new MySqlConnection(_connectionString);

            var sql = $"SELECT * FROM {TableName} WHERE {TableName}Id = @id;";

            var param = new DynamicParameters();
            param.Add("@id", id);

            var result = await connection.QuerySingleOrDefaultAsync<TEntity>(sql, param);

            return result;  
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var connection = new MySqlConnection(_connectionString);

            var sql = $"SELECT * FROM {TableName};";

            var result = await connection.QueryAsync<TEntity>(sql);

            return result.ToList();
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            var entity = await FindAsync(id);
            if (entity == null)
            {
                throw new NotfoundException();
            }
            return entity;
        }
    }
}
