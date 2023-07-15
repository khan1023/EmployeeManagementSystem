using EMS.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Application.User.Command
{
    public class DeleteUserCommand : IRequest<UserResponse>
    {
        public int Id { get; private set; }

        public DeleteUserCommand(int Id)
        {
            this.Id = Id;
        }
    }
}
