﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Demo.Domain
{
    public interface IDepartmentRepository : ICrudRepository<Department, Guid>
    {
    }
}