using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace EMS.Application.User.Query
{
    public class GetAllLocationsQuery : IRequest<List<SelectListItem>>
    {
    }
}
