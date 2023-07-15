using EMS.Application.Repository.User;
using EMS.Application.User.Query;
using EMS.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.Application.User.Handler
{
    public class CheckDuplicateUserHandler : IRequestHandler<CheckDuplicateQuery, int>
    {
        private readonly IUserQueryRepository _userQueryRepository;

        public CheckDuplicateUserHandler(IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
        }
        public async Task<int> Handle(CheckDuplicateQuery request, CancellationToken cancellationToken)
        {
            try { 
            return await _userQueryRepository.CheckDuplicateAsync(request.name, request.emailId, request.Id);
            }
            catch(Exception ex)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError, "Error While Get Locations");
            }
        }
    }
}
