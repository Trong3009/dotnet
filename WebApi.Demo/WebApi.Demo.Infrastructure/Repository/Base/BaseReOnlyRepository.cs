using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Demo.Application;
using WebApi.Demo.Domain;

namespace WebApi.Demo.Infrastructure
{
    public abstract class BaseReOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        protected readonly IUnitOfWork Uow;
        protected virtual string TableName { get; set; } = typeof(TEntity).Name;

        protected BaseReOnlyRepository(IUnitOfWork uow)
        {
            Uow = uow;
        }

        public async Task<TEntity?> FindAsync(TKey id)
        {

            var sql = $"SELECT * FROM {TableName} WHERE {TableName}Id = @id;";

            var param = new DynamicParameters();
            param.Add("@id", id);

            var result = await Uow.Connection.QuerySingleOrDefaultAsync<TEntity>(sql, param, transaction: Uow.Transaction);

            return result;  
        }

        public async Task<List<TEntity>> GetAllAsync()
        {

            var sql = $"SELECT * FROM {TableName};";

            var result = await Uow.Connection.QueryAsync<TEntity>(sql, transaction: Uow.Transaction);

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

        public async Task<(List<TEntity>, List<TKey>)> GetListAsync(IEnumerable<TKey> ids)
        {
            var sql = $"SELECT * FROM {TableName} WHERE {TableName}Id in @ids";

            var param = new DynamicParameters();
            param.Add("@ids", ids);

            var entities = (await Uow.Connection.QueryAsync<TEntity>(sql, param, transaction: Uow.Transaction)).ToList();
            var retrievedIds = entities.Select(entity => entity.GetId()).ToList();
            var remainingIds = ids.Except(retrievedIds).ToList();

            return (entities, remainingIds);
        }
    }
}
