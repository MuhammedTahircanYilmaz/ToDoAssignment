using Core.Repository;
using ToDoAssignment.Models.Entities;
using ToDoAssignment.Repository.Contexts;
using ToDoAssignment.Repository.Repositories.ToDos.Abstracts;

namespace ToDoAssignment.Repository.Repositories.ToDos.Concretes;

public class EfToDoRepository : EfRepositoryBase<BaseDbContext,ToDo,Guid> , IToDoRepository
{
    public EfToDoRepository(BaseDbContext context) : base(context)
    {

    }
}
