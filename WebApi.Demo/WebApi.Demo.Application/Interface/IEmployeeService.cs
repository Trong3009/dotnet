﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Demo.Application
{
    public interface IEmployeeService : ICrudService<Guid, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
        Task<bool> CheckDuplicateCodeAsync(string code);
    }
}
