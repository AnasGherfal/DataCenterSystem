using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Management.AdminsManagement.UpdateAdmin;

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