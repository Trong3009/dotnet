using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Demo.Domain;

namespace WebApi.Demo.Application
{
    public class DepartmentService : BaseRoadOnlyService<Department, Guid, DepartmentDto>, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository repository) : base(repository)
        {
            _departmentRepository = repository;
        }

        protected override DepartmentDto MapEntityDtoToEntityDto(Department entity)
        {
            var DepartmentDto = new DepartmentDto()
            {
                DepartmentId = entity.DepartmentId,
                DepartmentName = entity.DepartmentName,
            };
            return DepartmentDto;
        }
    }
}
