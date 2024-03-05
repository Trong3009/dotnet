using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Demo.Domain;

namespace WebApi.Demo.Application
{
    public abstract class BaseRoadOnlyService<TEntity,TKey, TEntityDto> : IReadOnlyService<TKey, TEntityDto> where TEntity : IEntity<TKey> where TEntityDto : class
    {
        protected readonly IReadOnlyRepository<TEntity, TKey> RoadOnlyRepository;

        public BaseRoadOnlyService(IReadOnlyRepository<TEntity, TKey> repository)
        {
            RoadOnlyRepository = repository;
        }

        public async Task<List<TEntityDto>> GetAllAsync()
        {
            var entities = await RoadOnlyRepository.GetAllAsync();
            var result = entities.Select(entity => MapEntityDtoToEntityDto(entity)).ToList();
            return result;
        }

        public async Task<TEntityDto> GetAsync(TKey id)
        {
            var entity = await RoadOnlyRepository.GetAsync(id);
            var result = MapEntityDtoToEntityDto(entity);
            return result;
        }
        public async Task<string> GetNewCodeAsync()
        {
            var result = await RoadOnlyRepository.GetNewCodeAsync();
            return result;
        }

        protected abstract TEntityDto MapEntityDtoToEntityDto(TEntity entity);
    }
}
