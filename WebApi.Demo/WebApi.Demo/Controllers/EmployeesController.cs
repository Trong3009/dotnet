
using Microsoft.AspNetCore.Mvc;

using WebApi.Demo.Application;

namespace WebApi.Demo.Controllers
{
    public class EmployeesController : BaseCrudController<Guid, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
        private readonly IEmployeeService _employeeService;
        protected EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        [Route("exist-code")]
        public async Task<bool> CheckExistCode(string code)
        {
            var result = await _employeeService.CheckDuplicateCodeAsync(code);
            return result;
        }
    }
}
