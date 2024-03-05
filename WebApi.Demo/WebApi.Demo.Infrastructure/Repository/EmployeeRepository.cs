using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Demo.Application;
using WebApi.Demo.Domain;
using static Dapper.SqlMapper;

namespace WebApi.Demo.Infrastructure
{
    public class EmployeeRepository : BaseCrudRepository<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<Employee?> FindByCodeAsync(string code)
        {

            var sql = $"SELECT * FROM Employee WHERE EmployeeId = @id;";

            var param = new DynamicParameters();
            param.Add("@code", code);

            var result = await Uow.Connection.QuerySingleOrDefaultAsync<Employee>(sql, param, transaction: Uow.Transaction);

            return result;
        }
    }
}
