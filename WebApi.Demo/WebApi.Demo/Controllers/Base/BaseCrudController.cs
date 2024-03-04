using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using WebApi.Demo.Application;

namespace WebApi.Demo
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseCrudController<TKey, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseRoadOnlyController<TKey,TEntityDto> where TEntityDto : class where TEntityCreateDto : class where TEntityUpdateDto : class
    {
        public readonly ICrudService<TKey, TEntityDto, TEntityCreateDto, TEntityUpdateDto> CrudService;
        protected BaseCrudController(ICrudService<TKey, TEntityDto, TEntityCreateDto, TEntityUpdateDto> crudService) : base(crudService)
        {
            CrudService = crudService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAsync(TEntityCreateDto entityCreateDto)
        {
            var result = await CrudService.InsertAsync(entityCreateDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("{id}")]

        public async Task<IActionResult> PutAsync(TKey id, TEntityUpdateDto entityUpdateDto)
        {
            var result = await CrudService.UpdateAsync(id, entityUpdateDto);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> DelateAsync(TKey id)
        {
            var result = await CrudService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpDelete]

        public async Task<IActionResult> DelateManyAsync(List<TKey> ids)
        {
            var result = await CrudService.DeleteManyAsync(ids);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
