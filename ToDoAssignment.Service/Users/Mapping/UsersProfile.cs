using AutoMapper;
using ToDoAssignment.Models.Users.Dtos.Response;
using ToDoAssignment.Models.Users.Entity;

namespace ToDoAssignment.Service.Users.Mapping
{
    public class UsersProfile : Profile

    {
        public UsersProfile()
        {
            CreateMap<User, UserResponseDto>();
        }
    }

}
