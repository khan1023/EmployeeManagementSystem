using EMS.Application.User.Query;
using EMS.Domain.Entities;
using EMS.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.Application.User.Handler
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDetails>
    {
        private readonly IMediator _mediator;

        public GetUserByIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<UserDetails> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id < 0)
            {
                throw new ApiException(StatusCodes.Status400BadRequest, "User Id Should be Greater then 0.");
            }
            var userDetails = await _mediator.Send(new GetAllUserQuery());
            try
            {
                var selecteduser = userDetails.FirstOrDefault(x => x.Id == request.Id);
                return selecteduser;
            }
            catch(Exception ex)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError, "Internal ServerError.");
            }
            
        }
    }
}
