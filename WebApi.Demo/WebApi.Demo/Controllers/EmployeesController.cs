using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace WebApi.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeAsync()
        {
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEmployeeAsync(Guid id)
        {
            

            if(result == null)
            {
                throw new Exception("Không tìm thấy");
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost]

        public async Task<IActionResult> CreateEmployeeAsync([FromBody] Employee employee)
        {
            var connectionString = "Server=localhost;Port=3306;Database=nqtrong.demo;Uid=root;Pwd=;";

            using MySqlConnection connection = new(connectionString);

            var sql = "INSERT INTO employee VALUES @EmployeeId @EmployeeCode @FullName @DateOfBirth @Gender";

            var parameters = new
            {
                EmployeeId = Guid.NewGuid(),
                employee.EmployeeCode,
                employee.FullName,
                employee.DateOfBirth,
                employee.Gender,
            };

           

            try
            {
                await connection.ExecuteAsync(sql, parameters);
                return StatusCode(StatusCodes.Status201Created);
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            
        }
    }
}
