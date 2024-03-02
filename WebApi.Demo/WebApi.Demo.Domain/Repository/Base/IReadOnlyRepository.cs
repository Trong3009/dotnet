using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Demo.Domain
{
    public interface IReadOnlyRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// Hàm lấy ra tất nhân viên
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllAsync();
        /// <summary>
        /// Hàm lấy ra 1 nhân viên
        /// </summary>
        /// <param name="EmpoloyeeId"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(TKey id);

        Task<TEntity?> FindAsync(TKey id);
    }
}
