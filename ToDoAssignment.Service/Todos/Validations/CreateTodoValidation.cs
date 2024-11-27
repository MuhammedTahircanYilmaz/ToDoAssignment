using FluentValidation;
using ToDoAssignment.Models.Todos.Dtos.Requests;

namespace ToDoAssignment.Service.Todos.Validations;

public class CreateTodoValidation : AbstractValidator<CreateToDoRequestDto>
{
    public CreateTodoValidation()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("The title cannot be empty!");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("The category name cannot be null or Empty");
        RuleFor(x => x.Priority).NotNull().WithMessage("The to do priority cannot be null");
        RuleFor(x => x.Description).NotEmpty().WithMessage("The description cannot be null");
    }
}