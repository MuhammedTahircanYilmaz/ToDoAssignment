using Core.Repository;
using ToDoAssignment.Models.Todos.Entity;
using ToDoAssignment.Repository.Contexts;
using ToDoAssignment.Repository.ToDos.Repositories.Abstracts;

namespace ToDoAssignment.Repository.ToDos.Repositories.Concretes;

public class EfToDoRepository : EfRepositoryBase<BaseDbContext, ToDo, Guid>, IToDoRepository
{
    public EfToDoRepository(BaseDbContext context) : base(context)
    {

    }
}
