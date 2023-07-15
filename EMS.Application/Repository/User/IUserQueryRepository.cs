
using EMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EMS.Application.Repository.User
{
    public interface IUserQueryRepository
    {
        //Custom operation which is not generic
        Task<List<UserDetails>> GetAllAsync();
        Task<UserDetails> GetByIdAsync(int id);
        Task<int> CheckDuplicateAsync(string name,string emailId,int id=0);
        Task<List<SelectListItem>> GetAllLocations();
    }
}
