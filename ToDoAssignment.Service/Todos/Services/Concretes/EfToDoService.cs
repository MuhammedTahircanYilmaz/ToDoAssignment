using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using ToDoAssignment.Models.Todos.Dtos.Requests;
using ToDoAssignment.Models.Todos.Dtos.Response;
using ToDoAssignment.Models.Todos.Entity;
using ToDoAssignment.Models.Todos.Enums;
using ToDoAssignment.Models.Users.Entity;
using ToDoAssignment.Repository.ToDos.Repositories.Abstracts;
using ToDoAssignment.Service.Constants;
using ToDoAssignment.Service.Rules;
using ToDoAssignment.Service.Todos.Abstracts;

namespace ToDoAssignment.Service.Todos.Concretes;

public class EfToDoService(IMapper mapper, IToDoRepository toDoRepository, ToDoBusinessRules businessRules) : IToDoService
{
    public ReturnModel<NoData> Add(CreateToDoRequestDto dto, string userId)
    {
        ToDo toDo = mapper.Map<ToDo>(dto);
        toDo.UserId = userId;
        toDo.Completed = false;

        toDoRepository.Add(toDo);
        return ReturnModel<NoData>.ReturnModelOfSuccess(null, 204, Messages.ToDoCreatedMessage);
    }

    public ReturnModel<NoData> Update(UpdateToDoRequestDto dto, string userId)
    {

        bool toDoExists = businessRules.ToDoExists(dto.Id);
        ToDo? toDo = toDoRepository.GetById(dto.Id);

        CheckUserMatches(toDo.UserId, userId);

        CheckUpdateable(toDo);

        toDo.Title = dto.Title;
        toDo.CategoryId = (Guid)dto.CategoryId;
        toDo.Description = dto.Description;
        toDo.Priority = (Priority)dto.Priority;

        toDoRepository.Update(toDo);
        return ReturnModel<NoData>.ReturnModelOfSuccess(null, 204, Messages.ToDoUpdatedMessage);
    }

    public ReturnModel<NoData> Delete(Guid id, string userId)
    {
        bool toDoExists = businessRules.ToDoExists(id);
        ToDo toDo = toDoRepository.GetById(id);

        CheckUserMatches(toDo.UserId, userId);

        toDoRepository.Delete(toDo);
        return ReturnModel<NoData>.ReturnModelOfSuccess(null, 204, Messages.ToDoDeletedMessage);

    }

    public ReturnModel<ToDoResponseDto> GetById(Guid id, string userId)
    {
        businessRules.ToDoExists(id);
        List<ToDo> toDo = toDoRepository.GetAll(x => x.UserId == userId && x.Id == id);

        var response = mapper.Map<ToDoResponseDto>(toDo);

        return ReturnModel<ToDoResponseDto>.ReturnModelOfSuccess(response, 200);
    }

    public ReturnModel<List<ToDoResponseDto>> GetAllByPriority(Priority priority, string userId)
    {
        List<ToDo> toDos = toDoRepository.GetAll(x => x.UserId == userId && x.Priority == priority);

        var response = mapper.Map<List<ToDoResponseDto>>(toDos);

        return ReturnModel<List<ToDoResponseDto>>.ReturnModelOfSuccess(response, 200);
    }

    public ReturnModel<List<ToDoResponseDto>> GetAllByUserId(string userId)
    {
        List<ToDo> toDos = toDoRepository.GetAll(x => x.UserId == userId);

        var response = mapper.Map<List<ToDoResponseDto>>(toDos);

        return ReturnModel<List<ToDoResponseDto>>.ReturnModelOfSuccess(response, 200);
    }

    public ReturnModel<List<ToDoResponseDto>> GetAllByStatus(bool completed, string userId)
    {
        List<ToDo> toDos = toDoRepository.GetAll(x => x.UserId == userId && x.Completed == completed);

        var response = mapper.Map<List<ToDoResponseDto>>(toDos);

        return ReturnModel<List<ToDoResponseDto>>.ReturnModelOfSuccess(response, 200);
    }

    public ReturnModel<List<ToDoResponseDto>> GetAllByTitleHas(string text, string userId)
    {
        List<ToDo> toDos = toDoRepository.GetAll(x => x.UserId == userId && x.Title.Contains(text, StringComparison.InvariantCultureIgnoreCase))
            .FindAll(x => x.Title.Contains(text, StringComparison.InvariantCultureIgnoreCase));

        var response = mapper.Map<List<ToDoResponseDto>>(toDos);

        return ReturnModel<List<ToDoResponseDto>>.ReturnModelOfSuccess(response, 200);
    }

    public ReturnModel<List<ToDoResponseDto>> GetAllByCategory(Guid categoryId, string userId)
    {
        List<ToDo> toDos = toDoRepository.GetAll(x => x.UserId == userId && x.CategoryId == categoryId);

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

    private void CheckUserMatches(string userId, string IdToBeChecked)
    {
        if (!(userId == IdToBeChecked))
        {
            throw new BusinessException("The ToDo does not belong to the user!");
        }
    }
}
