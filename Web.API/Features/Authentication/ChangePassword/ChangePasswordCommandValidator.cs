using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.Authentication.ChangePassword;

public class ChangePasswordCommandValidator: AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(c => c.OldPassword)
            .NotEmpty();
        RuleFor(c => c.NewPassword)
            .NotEmpty();
        RuleFor(c => c.ConfirmPassword)
            .NotEmpty()
            .Equal(p => p.NewPassword);
    }
}