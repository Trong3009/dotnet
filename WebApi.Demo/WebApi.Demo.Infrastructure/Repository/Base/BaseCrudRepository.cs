using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Demo.Domain;

namespace WebApi.Demo.Infrastructure
{
    public abstract class BaseCrudRepository<TEntity, TKey> : BaseReOnlyRepository<TEntity, TKey>, ICrudRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        protected BaseCrudRepository(string connectionString) : base(connectionString)
        {
        }

        public Task<TEntity> InsertEmpoloyeeAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            var connection = new MySqlConnection(_connectionString);

            var sql = $"DELETE FROM {TableName} WHERE {TableName}Id = @id;";

            var param = new DynamicParameters();
            param.Add("@id", entity.GetId());

            var result = await connection.ExecuteAsync(sql, param);

            return result;
        }

        public async Task<int> DeleteManyAsync(List<TEntity> entities)
        {
            var connection = new MySqlConnection(_connectionString);

            var sql = $"DELETE FROM {TableName} WHERE {TableName}Id IN @ids;";

            var param = new DynamicParameters();
            param.Add("ids", entities.Select(entity => entity.GetId()));

            var result = await connection.ExecuteAsync(sql, param);

            return result;
        }

    }
}
