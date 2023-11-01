using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Management.AdminsManagement.UnlockAdminById;

public class UnlockAdminByIdCommandValidator: AbstractValidator<UnlockAdminByIdCommand>
{
    public UnlockAdminByIdCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}