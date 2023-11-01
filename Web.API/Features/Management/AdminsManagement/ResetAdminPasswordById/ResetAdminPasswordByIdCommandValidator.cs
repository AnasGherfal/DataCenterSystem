using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Management.AdminsManagement.ResetAdminPasswordById;

public class ResetAdminPasswordByIdCommandValidator: AbstractValidator<ResetAdminPasswordByIdCommand>
{
    public ResetAdminPasswordByIdCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}