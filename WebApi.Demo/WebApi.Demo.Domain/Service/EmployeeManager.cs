using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Demo.Domain
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task CheckDuplicateCodeAsync(string code)
        {
            var employee = await _employeeRepository.FindByCodeAsync(code);
            if (employee is not null)
            {
                throw new ConflictException("Trungf max nhaan vieen");
            }
        }
    }
}
