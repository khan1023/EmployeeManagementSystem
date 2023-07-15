using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Exceptions
{
    public class ApiException : BaseException
    {
        public ApiException(int code,string message)
            :base(message)
        {
            Code = code;
        }
    }
}
