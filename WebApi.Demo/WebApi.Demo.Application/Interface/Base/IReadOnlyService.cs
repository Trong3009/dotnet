using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Demo.Application
{
    public interface IReadOnlyService<TKey, TEntityDto> where TEntityDto : class
    {
        Task<List<TEntityDto>> GetAllAsync();
        Task<TEntityDto> GetAsync(TKey id);
    }
}
