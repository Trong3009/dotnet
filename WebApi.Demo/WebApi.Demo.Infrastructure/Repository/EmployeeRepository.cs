using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Demo.Domain;

namespace WebApi.Demo.Infrastructure
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            var connectionString = "Server=localhost;Port=3306;Database=nqtrong.demo;Uid=root;Pwd=;";

            var connection = new MySqlConnection(connectionString);

            var sql = "SELECT * FROM employee;";

            var result = await connection.QueryAsync<Employee>(sql);

            return result.ToList();
        }

        public Task<Employee> GetEmployeeAsync(Guid EmpoloyeeId)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> FindEmployeeById(Guid EmployeeId)
        {
            var connectionString = "Server=localhost;Port=3306;Database=nqtrong.demo;Uid=root;Pwd=;";

            var connection = new MySqlConnection(connectionString);

            var sql = $"SELECT * FROM employee WHERE EmployeeId = @id;";

            var param = new DynamicParameters();
            param.Add("id", EmployeeId);

            var result = await connection.QuerySingleOrDefaultAsync<Employee>(sql, param);

            return result;
        }

        public Task<Employee> InsertEmpoloyeeAsync(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            throw new NotImplementedException();
        }
        public Task<int> DeleteEmployeeAsync(Employee employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteManyEmployeeAsync(List<Employee> employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
