using EMS.Application.Repository.User;
using EMS.Application.User.Query;
using EMS.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EMS.Application.User.Handler
{
    public class GetLoactionHandler : IRequestHandler<GetAllLocationsQuery, List<SelectListItem>>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        public GetLoactionHandler(IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
        }
        public async Task<List<SelectListItem>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _userQueryRepository.GetAllLocations();
            }
            catch(Exception ex)
            {
                throw new ApiException(StatusCodes.Status500InternalServerError, "Error While Get Locations");
            }
            
        }
    }
}
