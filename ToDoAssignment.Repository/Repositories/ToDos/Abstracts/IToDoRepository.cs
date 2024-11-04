using Core.Repository;
using ToDoAssignment.Models.Entities;

namespace ToDoAssignment.Repository.Repositories.ToDos.Abstracts;

public interface IToDoRepository : IRepository<ToDo, Guid>
{
}
