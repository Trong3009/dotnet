using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Demo.Domain
{
    public class Department : BaseAuditEntity, IEntity<Guid>
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public Guid GetId()
        {
            return DepartmentId;
        }

        public void SetId(Guid id)
        {
            DepartmentId = id;
        }
    }
}
