using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Application.User.Query
{
    public class CheckDuplicateQuery : IRequest<int>
    {
        public string name { get; private set; }
        public string emailId { get; private set; }
        public int Id { get; private set; }

        public CheckDuplicateQuery(string name, string emailId,int Id)
        {
            this.name = name;
            this.emailId = emailId;
            this.Id = Id;
        }
    }
}
