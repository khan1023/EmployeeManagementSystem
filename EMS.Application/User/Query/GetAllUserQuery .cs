using EMS.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace EMS.Application.User.Query
{
    public class GetAllUserQuery : IRequest<List<UserDetails>>
    {

    }
}
