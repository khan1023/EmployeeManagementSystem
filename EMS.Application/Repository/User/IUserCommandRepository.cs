using EMS.Application.Response;
using EMS.Domain.Entities;
using System.Threading.Tasks;

namespace EMS.Application.Repository.User
{
    public interface IUserCommandRepository
    {
        //Custom Command which is not generic
        Task<UserResponse> AddAsync(UserDetails obj);
        Task<UserResponse> UpdateAsync(UserDetails obj);
        Task<UserResponse> DeleteAsync(int id);
    }
}
