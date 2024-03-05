using AutoMapper;
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
        private readonly IEmployeeManager _employeeManager;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository repository, IMapper mapper, IEmployeeManager employeeManager) : base(repository)
        {
            _employeeRepository = repository;
            _mapper = mapper;
            _employeeManager = employeeManager;
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

        public override async Task<Employee> MapEntityCreateDtoToEntity(EmployeeCreateDto entityCreateDto)
        {
            await _employeeManager.CheckDuplicateCode(entityCreateDto.EmployeeCode);
            var employeeCreate = _mapper.Map<Employee>(entityCreateDto);
            employeeCreate.EmployeeId = Guid.NewGuid();
            return employeeCreate;
        }

        public override async Task<Employee> MapEntityUpdateDtoToEntity(Guid id,EmployeeUpdateDto entityUpdateDto)
        {
            var employee = await _employeeRepository.GetAsync(id);
            if (employee.EmployeeCode != entityUpdateDto.EmployeeCode)
            {
                await _employeeManager.CheckDuplicateCode(entityUpdateDto.EmployeeCode);
            }
            var employeeUpdate = _mapper.Map<Employee>(entityUpdateDto);
            return employeeUpdate;
        }

        protected override EmployeeDto MapEntityDtoToEntityDto(Employee entity)
        {
            var entityDto = _mapper.Map<EmployeeDto>(entity);
            
            return entityDto;
            
        }
    }
}
