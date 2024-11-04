using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using ToDoAssignment.Models.Dtos.Todos.Requests;
using ToDoAssignment.Models.Dtos.Todos.Response;
using ToDoAssignment.Models.Entities;
using ToDoAssignment.Models.Enums;
using ToDoAssignment.Repository.Repositories.ToDos.Abstracts;
using ToDoAssignment.Service.Constants;
using ToDoAssignment.Service.Rules;
using ToDoAssignment.Service.Services.Todos.Abstracts;

namespace ToDoAssignment.Service.Services.Todos.Concretes;

public class EfToDoService(IMapper mapper, IToDoRepository toDoRepository, ToDoBusinessRules businessRules) : ITodoService
{
    public ReturnModel<NoData> Add(CreateToDoRequestDto dto, string userId)
    {
        ToDo toDo = mapper.Map<ToDo>(dto);
        toDo.UserId = userId;

        toDoRepository.Add(toDo);
        return ReturnModel<NoData>.ReturnModelOfSuccess(null, 200, Messages.ToDoCreatedMessage);
    }

    public ReturnModel<NoData> Update(UpdateToDoRequestDto dto)
    {
        bool toDoExists = businessRules.ToDoExists(dto.Id);
        ToDo? toDo = toDoRepository.GetById(dto.Id);

        CheckUpdateable(toDo);

        toDo.Title = dto.Title;
        toDo.CategoryId = (Guid)dto.CategoryId;
        toDo.Description = dto.Description;
        toDo.Priority = (Priority)dto.Priority;

        toDoRepository.Update(toDo);
        return ReturnModel<NoData>.ReturnModelOfSuccess(null, 200, Messages.ToDoUpdatedMessage);
    }

    public ReturnModel<NoData> Delete(Guid id)
    {
        bool toDoExists = businessRules.ToDoExists(id);
        ToDo todo = toDoRepository.GetById(id);

        toDoRepository.Delete(todo);
        return ReturnModel<NoData>.ReturnModelOfSuccess(null,204, Messages.ToDoDeletedMessage);
        

    }

    public ReturnModel<ToDoResponseDto> GetById(Guid id)
    {
        businessRules.ToDoExists(id);
        ToDo todo = toDoRepository.GetById(id);
        var response = mapper.Map<ToDoResponseDto>(todo);

        return ReturnModel<ToDoResponseDto>.ReturnModelOfSuccess(response, 200);
    }

    public ReturnModel<List<ToDoResponseDto>> GetAllByPriority(Priority priority)
    {
        List<ToDo> toDos = toDoRepository.GetAll(x => x.Priority == priority);

        var response = mapper.Map<List<ToDoResponseDto>>(toDos);

        return ReturnModel<List<ToDoResponseDto>>.ReturnModelOfSuccess(response, 200);
    }

    public ReturnModel<List<ToDoResponseDto>> GetAllByUserId(string id)
    {
        List<ToDo> toDos = toDoRepository.GetAll(x => x.UserId == id);

        var response = mapper.Map<List<ToDoResponseDto>>(toDos);

        return ReturnModel<List<ToDoResponseDto>>.ReturnModelOfSuccess(response, 200);
    }

    public ReturnModel<List<ToDoResponseDto>> GetAllByStatus(bool completed)
    {
        List<ToDo> toDos = toDoRepository.GetAll(x => x.Completed == completed);

        var response = mapper.Map<List<ToDoResponseDto>>(toDos);

        return ReturnModel<List<ToDoResponseDto>>.ReturnModelOfSuccess(response, 200);
    }

    public ReturnModel<List<ToDoResponseDto>> GetAllByTitleHas(string text)
    {
        List<ToDo> toDos = toDoRepository.GetAll(x => x.Title.Contains(text, StringComparison.InvariantCultureIgnoreCase));

        var response = mapper.Map<List<ToDoResponseDto>>(toDos);

        return ReturnModel<List<ToDoResponseDto>>.ReturnModelOfSuccess(response, 200);
    }

    public ReturnModel<List<ToDoResponseDto>> GetAllByCategory(Guid categoryId)
    {
        List<ToDo> toDos = toDoRepository.GetAll(x => x.CategoryId == categoryId);

        var response = mapper.Map<List<ToDoResponseDto>>(toDos);

        return ReturnModel<List<ToDoResponseDto>>.ReturnModelOfSuccess(response, 200);
    }

    public ReturnModel<List<ToDoResponseDto>> GetAll()
    {
        List<ToDo> todos = toDoRepository.GetAll();

        var response = mapper.Map<List<ToDoResponseDto>>(todos);
        return ReturnModel<List<ToDoResponseDto>>.ReturnModelOfSuccess(response, 200);
    }

    private void CheckUpdateable(ToDo toDo)
    {
        if (!toDo.IsUpdateable)
        {
            throw new BusinessException("The ToDo is not updateable");
        }
    }
}
