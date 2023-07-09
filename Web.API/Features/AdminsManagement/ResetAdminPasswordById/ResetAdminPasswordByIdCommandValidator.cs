using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.AdminsManagement.ResetAdminPasswordById;

public class ResetAdminPasswordByIdCommandValidator: AbstractValidator<ResetAdminPasswordByIdCommand>
{
    public ResetAdminPasswordByIdCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}