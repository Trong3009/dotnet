using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Demo.Domain;
using static Dapper.SqlMapper;

namespace WebApi.Demo.Infrastructure
{
    public class EmployeeRepository : BaseCrudRepository<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Employee?> FindByCodeAsync(string code)
        {
            var connection = new MySqlConnection(_connectionString);

            var sql = $"SELECT * FROM Employee WHERE EmployeeId = @id;";

            var param = new DynamicParameters();
            param.Add("@code", code);

            var result = await connection.QuerySingleOrDefaultAsync<Employee>(sql, param);

            return result;
        }
    }
}
