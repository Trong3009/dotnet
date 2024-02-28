using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Demo.Application
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Hàm lấy ra tất nhân viên
        /// </summary>
        /// <returns></returns>
        Task<List<EmployeeDto>> GetAllEmployeeAsync();
        /// <summary>
        /// Hàm lấy ra 1 nhân viên
        /// </summary>
        /// <param name="EmpoloyeeId"></param>
        /// <returns></returns>
        Task<EmployeeDto> GetEmployeeAsync(Guid employeeId);
        /// <summary>
        /// Hàm tạo ra mới nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<EmployeeDto> InsertEmpoloyeeAsync(EmployeeCreateDto employeeCreateDto);
        /// <summary>
        /// Hàm suwear nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<EmployeeDto> UpdateEmployeeAsync(Guid EmployeeId,EmployeeUpdateDto employeeUpdateDto);
        /// <summary>
        /// Hàm xoa nhân viên theo @id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<int> DeleteEmployeeAsync(Guid employeeId);
        Task<int> DeleteManyEmployeeAsync(List<Guid> employeeIds);
    }
}
