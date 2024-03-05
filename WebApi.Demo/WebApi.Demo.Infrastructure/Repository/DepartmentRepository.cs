using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Demo.Application;
using WebApi.Demo.Domain;

namespace WebApi.Demo.Infrastructure
{
    public class DepartmentRepository : BaseReOnlyRepository<Department, Guid>, IDepartmentRepository
    {
        public new string TableName = "Department";
        public DepartmentRepository(IUnitOfWork uow) : base(uow)
        {
        }
    }
}
