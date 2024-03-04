using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Demo.Application
{
    public interface ICrudService<TKey, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IReadOnlyService<TKey,TEntityDto> where TEntityDto : class where TEntityCreateDto : class where TEntityUpdateDto : class
    {
        Task<TEntityDto> InsertAsync(TEntityCreateDto entityCreateDto);
        /// <summary>
        /// Hàm suwear Banr ghi
        /// </summary>
        /// <param name="entity">ban ghi duoc them</param>
        /// <returns></returns>
        Task<TEntityDto> UpdateAsync(TKey id, TEntityUpdateDto entityUpdateDto);
        /// <summary>
        /// Hàm xoa nhân viên theo @id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(TKey id);
        Task<int> DeleteManyAsync(List<TKey> ids);
    }
}
