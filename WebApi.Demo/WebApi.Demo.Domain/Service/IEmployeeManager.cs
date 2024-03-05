using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Demo.Domain
{
    public interface IEmployeeManager
    {
        Task CheckDuplicateCodeAsync(string code);
    }
}
