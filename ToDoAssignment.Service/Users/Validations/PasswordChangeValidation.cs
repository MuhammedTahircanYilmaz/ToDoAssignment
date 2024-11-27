using FluentValidation;
using ToDoAssignment.Models.Users.Dtos.Requests;

namespace ToDoAssignment.Service.Users.Validations;

public class PasswordChangeValidation : AbstractValidator<ChangePasswordRequestDto>
{
    public PasswordChangeValidation()
    {
        RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("The current password cannot be empty");
        RuleFor(x => x.CurrentPassword).MinimumLength(8).WithMessage("The current password cannot be shorter than 8 characters");
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage("The new password cannot be empty");
        RuleFor(x => x.NewPassword).MinimumLength(8).WithMessage("The new password cannot be shorter than 8 characters");
    }
}
