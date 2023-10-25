using FluentValidation;

namespace Web.API.Features.Consumer.Login;

public class LoginCommandValidator: AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(c => c.Password)
            .NotEmpty();
    }
}