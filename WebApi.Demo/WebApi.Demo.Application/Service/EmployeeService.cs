using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Demo.Domain;

namespace WebApi.Demo.Application
{
    public class EmployeeService : BaseCrudService<Employee, Guid, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository repository) : base(repository)
        {
            _employeeRepository = repository;
        }

        public async Task<bool> CheckDuplicateCodeAsync(string code)
        {
            var employee = await _employeeRepository.FindByCodeAsync(code);
            if (employee == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override Task<Employee> MapEntityCreateDtoToEntity(EmployeeCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public override Task<Employee> MapEntityUpdateDtoToEntity(EmployeeUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }

        protected override EmployeeDto MapEntityDtoToEntityDto(Employee entity)
        {
            var entityDto = new EmployeeDto()
            {
                EmployeeId = entity.EmployeeId,
                EmployeeCode = entity.EmployeeCode,
                FullName = entity.FullName,
                DateOfBirth = entity.DateOfBirth,
                Gender = entity.Gender,
            };
            return entityDto;
            
        }
    }
}
