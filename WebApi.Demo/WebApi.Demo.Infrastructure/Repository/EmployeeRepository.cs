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
        private readonly string _connectionString;
        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            var connection = new MySqlConnection(_connectionString);

            var sql = "SELECT * FROM employee;";

            var result = await connection.QueryAsync<Employee>(sql);

            return result.ToList();
        }

        public async Task<Employee> GetEmployeeAsync(Guid employeeId)
        {
            var employee = await FindEmployeeAsync(employeeId);
            if(employee == null)
            {
                throw new NotfoundException();
            }
            return employee;
        }

        public async Task<Employee?> FindEmployeeAsync(Guid EmployeeId)
        {

            var connection = new MySqlConnection(_connectionString);

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
        public async Task<int> DeleteEmployeeAsync(Employee employee)
        {
            var connection = new MySqlConnection(_connectionString);

            var sql = $"DELETE FROM employee WHERE EmployeeId = @id;";

            var param = new DynamicParameters();
            param.Add("id", employee.EmployeeId);

            var result = await connection.ExecuteAsync(sql, param);

            return result;
        }

        public async Task<int> DeleteManyEmployeeAsync(List<Employee> employees)
        {
            var connection = new MySqlConnection(_connectionString);

            var sql = $"DELETE FROM employee WHERE EmployeeId IN @ids;";

            var param = new DynamicParameters();
            param.Add("ids", employees.Select(employee => employee.EmployeeId));

            var result = await connection.ExecuteAsync(sql, param);

            return result;
        }
    }
}
