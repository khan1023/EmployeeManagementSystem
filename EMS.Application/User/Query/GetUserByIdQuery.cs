using EMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Application.User.Query
{
    public class GetUserByIdQuery : IRequest<UserDetails>
    {
        public int Id { get; private set; }

        public GetUserByIdQuery(int Id)
        {
            this.Id = Id;
        }

    }
}
