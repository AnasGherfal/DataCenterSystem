using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.AdminsManagement.UnlockAdminById;

public class UnlockAdminByIdCommandValidator: AbstractValidator<UnlockAdminByIdCommand>
{
    public UnlockAdminByIdCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}