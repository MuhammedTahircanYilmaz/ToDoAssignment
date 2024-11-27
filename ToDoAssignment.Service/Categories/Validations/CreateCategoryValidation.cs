using FluentValidation;
using ToDoAssignment.Models.Categories.Dtos.Requests;

namespace ToDoAssignment.Service.Categories.Validations;

public class CreateCategoryValidation : AbstractValidator<AddCategoryRequestDto>
{
    public CreateCategoryValidation()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("The category name cannot be empty!");
    }
}
