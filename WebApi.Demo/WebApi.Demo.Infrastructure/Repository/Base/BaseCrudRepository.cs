using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Demo.Application;
using WebApi.Demo.Domain;

namespace WebApi.Demo.Infrastructure
{
    public abstract class BaseCrudRepository<TEntity, TKey> : BaseReOnlyRepository<TEntity, TKey>, ICrudRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        protected BaseCrudRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var entityType = typeof(TEntity);
            var properties = entityType.GetProperties();

            var columnNames = string.Join(", ", properties.Select(prop => prop.Name));
            var parameterNames = string.Join(", ", properties.Select(prop => $"@{prop.Name}"));

            // Viết lệnh truy vấn để thêm mới bản ghi
            var sql = $"INSERT INTO {TableName} ({columnNames}) VALUES ({parameterNames})";

            var rowsAffected = await Uow.Connection.ExecuteAsync(sql, entity, transaction: Uow.Transaction);

            if (rowsAffected == 0)
            {
                throw new Exception($"Thêm bản ghi vào bảng {TableName} thất bại!");
            }

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            // Sử dụng Reflection để truy xuất các thuộc tính của entity
            var entityType = typeof(TEntity);
            var properties = entityType.GetProperties();

            var assignments = string.Join(", ", properties.Select(prop => $"{prop.Name} = @{prop.Name}"));

            // Viết lệnh truy vấn để cập nhật bản ghi
            var sql = $"UPDATE {TableName} SET {assignments} WHERE {TableName}Id = @{TableName}Id;";

            var rowsAffected = await Uow.Connection.ExecuteAsync(sql, entity, transaction: Uow.Transaction);

            if (rowsAffected == 0)
            {
                throw new Exception($"Cập nhật giá trị bản ghi cho bảng {TableName} không thành công!");
            }
            return entity;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {

            var sql = $"DELETE FROM {TableName} WHERE {TableName}Id = @id;";

            var param = new DynamicParameters();
            param.Add("@id", entity.GetId());

            var result = await Uow.Connection.ExecuteAsync(sql, param, transaction: Uow.Transaction);

            return result;
        }

        public async Task<int> DeleteManyAsync(List<TEntity> entities)
        {

            var sql = $"DELETE FROM {TableName} WHERE {TableName}Id IN @ids;";

            var param = new DynamicParameters();
            param.Add("ids", entities.Select(entity => entity.GetId()));

            var result = await Uow.Connection.ExecuteAsync(sql, param, transaction: Uow.Transaction);

            return result;
        }

    }
}
