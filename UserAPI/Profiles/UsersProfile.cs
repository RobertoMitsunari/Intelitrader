using AutoMapper;
using UserAPI.Dtos;
using UserAPI.Models;

namespace UserAPI.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            //Source -> Target
            CreateMap<User, UserReadDto>();
            //
            CreateMap<UserCreateDto,User>();
            CreateMap<UserUpdateDto,User>();
        }
    }
}