using AutoMapper;
using ToDoAssignment.Models.Todos.Dtos.Requests;
using ToDoAssignment.Models.Todos.Dtos.Response;
using ToDoAssignment.Models.Todos.Entity;
using ToDoAssignment.Service.Todos.Validations;

namespace ToDoAssignment.Service.Todos.Mapping;

public class ToDoProfile : Profile
{
    public ToDoProfile()
    {
        CreateMap<ToDo, ToDoResponseDto>();
        CreateMap<CreateToDoRequestDto, ToDo>();
    }
}
