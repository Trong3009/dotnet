using Microsoft.AspNetCore.Mvc;
using WebApi.Demo.Application;

namespace WebApi.Demo
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseRoadOnlyController<TKey, TEntityDto> : ControllerBase where TEntityDto : class
    {
        public readonly IReadOnlyService<TKey, TEntityDto> ReadOnlyService;
        protected BaseRoadOnlyController(IReadOnlyService<TKey, TEntityDto> readOnlyService)
        {
            ReadOnlyService = readOnlyService;
        }

        /// <summary>
        /// Hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await ReadOnlyService.GetAllAsync();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Hàm lấy bản ghi theo @id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(TKey id)
        {
            var result = await ReadOnlyService.GetAsync(id);

            if (result == null)
            {
                throw new Exception("Không tìm thấy");
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
