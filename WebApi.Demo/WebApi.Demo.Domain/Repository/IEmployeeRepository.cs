using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Demo.Domain
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Hàm lấy ra tất nhân viên
        /// </summary>
        /// <returns></returns>
        Task<List<Employee>> GetAllEmployeeAsync();
        /// <summary>
        /// Hàm lấy ra 1 nhân viên
        /// </summary>
        /// <param name="EmpoloyeeId"></param>
        /// <returns></returns>
        Task<Employee> GetEmployeeAsync(Guid employeeId);

        Task<Employee?> FindEmployeeAsync(Guid employeeId);
        /// <summary>
        /// Hàm tạo ra mới nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<Employee> InsertEmpoloyeeAsync(Employee employee);
        /// <summary>
        /// Hàm suwear nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        /// <summary>
        /// Hàm xoa nhân viên theo @id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<int> DeleteEmployeeAsync(Employee employee);

        Task<int> DeleteManyEmployeeAsync(List<Employee> employees);
    }
}
