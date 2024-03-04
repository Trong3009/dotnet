using WebApi.Demo.Application;

namespace WebApi.Demo
{
    public class DepartmentsController : BaseRoadOnlyController<Guid, DepartmentDto>
    {
        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {
        }
    }
}
