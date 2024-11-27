using Core.Entities;
using ToDoAssignment.Models.Todos.Dtos.Requests;
using ToDoAssignment.Models.Todos.Dtos.Response;
using ToDoAssignment.Models.Todos.Enums;

namespace ToDoAssignment.Service.Todos.Abstracts;

public interface IToDoService
{
    ReturnModel<NoData> Add(CreateToDoRequestDto dto, string userId);
    ReturnModel<NoData> Update(UpdateToDoRequestDto dto, string userId);
    ReturnModel<NoData> Delete(Guid id, string userId);
    ReturnModel<ToDoResponseDto> GetById(Guid id, string userId);
    ReturnModel<List<ToDoResponseDto>> GetAllByPriority(Priority priority, string userId);
    ReturnModel<List<ToDoResponseDto>> GetAllByUserId(string id);
    ReturnModel<List<ToDoResponseDto>> GetAllByStatus(bool completed, string userId);
    ReturnModel<List<ToDoResponseDto>> GetAllByTitleHas(string text, string userId);
    ReturnModel<List<ToDoResponseDto>> GetAllByCategory(Guid categoryId, string userId);
    ReturnModel<List<ToDoResponseDto>> GetAll();


}
