using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Demo.Domain;

namespace WebApi.Demo.Application
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private Guid employeeId;

        public EmployeeService(IEmployeeRepository employeeRepository) 
        {
            _employeeRepository = employeeRepository;  
        }
        public async Task<List<EmployeeDto>> GetAllEmployeeAsync()
        {
            var employees = await _employeeRepository.GetAllEmployeeAsync();
            var employeesDto = employees.Select(employee => EmployeeDtoToEmployeeDto(employee)).ToList();
            return employeesDto;
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeAsync(employeeId);
            var employeeDto = EmployeeDtoToEmployeeDto(employee);
            return employeeDto;
        }

        public Task<EmployeeDto> InsertEmpoloyeeAsync(EmployeeCreateDto employeeCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto> UpdateEmployeeAsync(Guid EmployeeId, EmployeeUpdateDto employeeUpdateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteEmployeeAsync(Guid employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeAsync(employeeId);
            var result = await _employeeRepository.DeleteEmployeeAsync(employee);
            return result;
        }

        public async Task<int> DeleteManyEmployeeAsync(List<Guid> employeeIds)
        {
            throw new NotImplementedException();
        }
        private EmployeeDto EmployeeDtoToEmployeeDto(Employee employee) 
        {
            var employeeDto = new EmployeeDto();
            return employeeDto;
        }
    }
}
