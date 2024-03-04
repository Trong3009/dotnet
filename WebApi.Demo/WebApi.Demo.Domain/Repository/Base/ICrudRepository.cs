using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Demo.Domain
{
    public interface ICrudRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        Task<TEntity> InsertAsync(TEntity entity);
        /// <summary>
        /// Hàm suwear Banr ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);
        /// <summary>
        /// Hàm xoa nhân viên theo @id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(TEntity entity);

        Task<int> DeleteManyAsync(List<TEntity> entities);
    }
}
