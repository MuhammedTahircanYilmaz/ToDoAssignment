using Core.Entities;
using ToDoAssignment.Models.Dtos.Todos.Requests;
using ToDoAssignment.Models.Dtos.Todos.Response;
using ToDoAssignment.Models.Entities;
using ToDoAssignment.Models.Enums;

namespace ToDoAssignment.Service.Services.Todos.Abstracts;

public interface ITodoService
{
    ReturnModel<NoData> Add(CreateToDoRequestDto dto, string userId);
    ReturnModel<NoData> Update(UpdateToDoRequestDto dto);
    ReturnModel<NoData> Delete(Guid id);
    ReturnModel<ToDoResponseDto> GetById(Guid id);
    ReturnModel<List<ToDoResponseDto>> GetAllByPriority(Priority priority);
    ReturnModel<List<ToDoResponseDto>> GetAllByUserId(string id);
    ReturnModel<List<ToDoResponseDto>> GetAllByStatus(bool completed);
    ReturnModel<List<ToDoResponseDto>> GetAllByTitleHas(string text);
    ReturnModel<List<ToDoResponseDto>> GetAllByCategory(Guid categoryId);
    ReturnModel<List<ToDoResponseDto>> GetAll();


}
