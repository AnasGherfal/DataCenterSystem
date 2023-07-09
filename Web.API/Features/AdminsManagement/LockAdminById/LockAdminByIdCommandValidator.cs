using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.AdminsManagement.LockAdminById;

public class LockAdminByIdCommandValidator: AbstractValidator<LockAdminByIdCommand>
{
    public LockAdminByIdCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}