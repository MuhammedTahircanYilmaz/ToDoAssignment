using Core.Exceptions;
using ToDoAssignment.Repository.Repositories.ToDos.Abstracts;
using ToDoAssignment.Service.Constants;

namespace ToDoAssignment.Service.Rules;

public class ToDoBusinessRules(IToDoRepository toDoRepository)
{
    public virtual bool ToDoExists(Guid id)
    {
        var toDo = toDoRepository.GetById(id);
        if (toDo is null)
        {
            throw new NotFoundException(Messages.ToDoIsNotPresentMessage(id), null);
        }
        return true;
    }
}

