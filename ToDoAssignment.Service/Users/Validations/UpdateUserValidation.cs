using FluentValidation;
using ToDoAssignment.Models.Users.Dtos.Requests;

namespace ToDoAssignment.Service.Users.Validations;

public class UpdateUserValidation : AbstractValidator<UpdateUserRequestDto>
{
    public UpdateUserValidation()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("The username cannot be empty");
    }
}
