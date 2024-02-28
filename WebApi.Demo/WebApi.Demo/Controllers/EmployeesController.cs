using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using WebApi.Demo.Application;

namespace WebApi.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeAsync()
        {
            var result = await _employeeService.GetAllEmployeeAsync();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEmployeeAsync(Guid id)
        {
            var result = await _employeeService.GetEmployeeAsync(id);

            if(result == null)
            {
                throw new Exception("Không tìm thấy");
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> DelateEmployeeAsync(Guid id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        [HttpDelete]

        public async Task<IActionResult> DelateManyEmployeeAsync(List<Guid> ids)
        {
            var result = await _employeeService.DeleteManyEmployeeAsync(ids);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
