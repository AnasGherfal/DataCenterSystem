using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.AdminsManagement.UpdateAdmin;

public class UpdateAdminCommandValidator: AbstractValidator<UpdateAdminCommand>
{
    public UpdateAdminCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
        RuleFor(p => p.Permissions)
            .NotNull()
            .Must(p => p > 0)
            .IsInEnum();
    }
}