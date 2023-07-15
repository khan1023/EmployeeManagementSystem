using EMS.Application.Repository.User;
using EMS.Application.User.Query;
using EMS.Domain.Entities;
using EMS.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.Application.User.Handler
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, List<UserDetails>>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        public GetAllUserHandler(IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
            
        }
        public async Task<List<UserDetails>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return (List<UserDetails>)await _userQueryRepository.GetAllAsync();
            }
            catch(Exception ex)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError, "Error while Getting User Details");
            }
        }
    }
}
