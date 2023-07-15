using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Application.Response
{
    public class UserResponse
    {
        public int DataId { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public bool isduplicate { get; set; }

    }
}
