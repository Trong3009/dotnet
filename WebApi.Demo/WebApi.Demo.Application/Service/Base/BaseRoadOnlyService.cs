using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Demo.Domain;

namespace WebApi.Demo.Application
{
    public class BaseRoadOnlyService<TEntity,TKey, TEntityDto> : IReadOnlyService<TKey, TEntityDto> where TEntity : IEntity<TKey> where TEntityDto : class
    {
        protected readonly IReadOnlyRepository<TEntity, TKey> Repository;

        public BaseRoadOnlyService(IReadOnlyRepository<TEntity, TKey> repository)
        {
            Repository = repository;
        }

        public Task<List<TEntityDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntityDto> GetAsync(TKey entityId)
        {
            throw new NotImplementedException();
        }
    }
}
