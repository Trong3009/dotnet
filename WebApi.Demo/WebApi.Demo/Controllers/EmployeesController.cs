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
            var connectionString = "Server=localhost;Port=3306;Database=nqtrong.demo;Uid=root;Pwd=;";

            var connection = new MySqlConnection(connectionString);

            var sql = "SELECT * FROM employee;";

            var result = await connection.QueryAsync<Employee>(sql);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEmployeeAsync(Guid id)
        {
            var connectionString = "Server=localhost;Port=3306;Database=nqtrong.demo;Uid=root;Pwd=;";

            var connection = new MySqlConnection(connectionString);

            var sql = $"SELECT * FROM employee WHERE EmployeeId = @id;";

            var param = new DynamicParameters();
            param.Add("id", id);

            var result = await connection.QuerySingleOrDefaultAsync<Employee>(sql, param);

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

            var connection = new MySqlConnection(connectionString);

            var sql = "INSERT INTO employee (EmployeeCode, FullName, Gender)";

            var parameters = new
            {
                employee.EmployeeCode,
                employee.FullName,
                employee.Gender
            };

            var result = await connection.ExecuteAsync(sql, parameters);

            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
