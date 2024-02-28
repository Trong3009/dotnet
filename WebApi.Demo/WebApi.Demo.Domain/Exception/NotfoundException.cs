using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Demo.Domain
{
    public class NotfoundException : Exception
    {
        public int ErrorCode { get; set; }
        public NotfoundException() { }
        public NotfoundException(int errorCode) 
        {
            ErrorCode = errorCode;
        }
        public NotfoundException(string message) : base(message) { }
        public NotfoundException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

    }
}
