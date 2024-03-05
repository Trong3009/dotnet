using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Demo.Domain.UnitTests.Fake
{
    internal class EmployeeRepositoryFake : IEmployeeRepository
    {
        public Task<int> DeleteAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteManyAsync(List<Employee> entities)
        {
            throw new NotImplementedException();
        }

        public Task<Employee?> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee?> FindByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<(List<Employee>, List<Guid>)> GetListAsync(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> InsertAsync(Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> UpdateAsync(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
