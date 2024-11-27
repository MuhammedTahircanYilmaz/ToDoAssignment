using Core.Repository;
using ToDoAssignment.Models.Todos.Entity;

namespace ToDoAssignment.Repository.ToDos.Repositories.Abstracts;

public interface IToDoRepository : IRepository<ToDo, Guid>
{
}
