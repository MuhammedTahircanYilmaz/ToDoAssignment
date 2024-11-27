using FluentValidation;
using ToDoAssignment.Models.Users.Dtos.Requests;

namespace ToDoAssignment.Service.Users.Validations;

public class RegisterValidation : AbstractValidator<RegisterUserRequestDto>
{
    public RegisterValidation()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("The email format is incorrect");
        RuleFor(x => x.Password).MinimumLength(8).WithMessage("The Passwords has to be at least eight characters long");
        RuleFor(x => x.Username).NotEmpty().WithMessage("The Username cannot be empty");
    }
}
