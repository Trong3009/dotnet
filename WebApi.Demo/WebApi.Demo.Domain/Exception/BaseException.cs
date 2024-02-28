using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApi.Demo.Domain
{
    public class BaseException
    {
        #region Properties
        public int ErrorCode { get; set; }
        public string? DevMessage { get; set; }
        public string? UserMassage { get; set; }
        public string? TraceId { get; set; }
        public string? MoreInfo { get; set; }
        public object? Errors { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        } 
        #endregion
    }
}
