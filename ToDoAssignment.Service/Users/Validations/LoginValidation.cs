using FluentValidation;
using ToDoAssignment.Models.Users.Dtos.Requests;

namespace ToDoAssignment.Service.Users.Validations;

public class LoginValidation : AbstractValidator<LoginRequestDto>
{ 
    public LoginValidation()
    {
        RuleFor(x => x.Password).MinimumLength(8).WithMessage("The Passwords has to be at least eight characters long");
    }
}
