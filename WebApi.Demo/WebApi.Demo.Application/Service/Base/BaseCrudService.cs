using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebApi.Demo.Domain;

namespace WebApi.Demo.Application
{
    public abstract class BaseCrudService<TEntity, TKey, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseRoadOnlyService<TEntity, TKey, TEntityDto>, ICrudService<TKey, TEntityDto, TEntityCreateDto, TEntityUpdateDto> where TEntityDto : class where TEntityCreateDto : class where TEntityUpdateDto : class where TEntity : IEntity<TKey>
    {
        protected readonly ICrudRepository<TEntity, TKey> CrudRepository;
        protected BaseCrudService(ICrudRepository<TEntity, TKey> repository) : base(repository)
        {
            CrudRepository = repository;
        }

        public async Task<TEntityDto> InsertAsync(TEntityCreateDto entityCreateDto)
        {
            var entity = await MapEntityCreateDtoToEntity(entityCreateDto);
            if(entity is BaseAuditEntity auditEntity)
            {
                auditEntity.CreatedBy = "Nguyen quy trong";
                auditEntity.CreatedDate = DateTime.Now;
            }
            await CrudRepository.InsertAsync(entity);
            var result = MapEntityDtoToEntityDto(entity);
            return result;
        }

        public async Task<TEntityDto> UpdateAsync(TKey id, TEntityUpdateDto entityUpdateDto)
        {
            var entity = await MapEntityUpdateDtoToEntity(id, entityUpdateDto);
            if (entity is BaseAuditEntity auditEntity)
            {
                auditEntity.ModifiedBy = "Nguyen quy trong";
                auditEntity.ModifiedDate = DateTime.Now;
            }
            await CrudRepository.UpdateAsync(entity);
            var result = MapEntityDtoToEntityDto(entity);
            return result;
        }

        public async Task<int> DeleteAsync(TKey id)
        {
            var entity = await CrudRepository.GetAsync(id);
            var result = await CrudRepository.DeleteAsync(entity);
            return result;

        }

        public async Task<int> DeleteManyAsync(List<TKey> ids)
        {
            var entities = new List<TEntity>();
            var listIds = new List<TKey>();
            (entities, listIds) = await CrudRepository.GetListAsync(ids);
            if (entities.Count == ids.ToList().Count)
            {
                var result = await CrudRepository.DeleteManyAsync(entities);
                return result;
            }
            else
            {
                throw new NotfoundException();
            }
        }

        public abstract Task<TEntity> MapEntityCreateDtoToEntity(TEntityCreateDto entityCreateDto);
        public abstract Task<TEntity> MapEntityUpdateDtoToEntity(TKey id ,TEntityUpdateDto entityUpdateDto);
    }
}
