using AutoMapper;
using EMS.Application.Response;
using EMS.Application.User.Command;
using EMS.Domain.Entities;

namespace EMS.Application.Mapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserDetails, UserResponse>().ReverseMap();
            CreateMap<UserDetails, CreateUserCommand>().ReverseMap();
            CreateMap<UserDetails, EditUserCommand>().ReverseMap();
        }
    }
}
