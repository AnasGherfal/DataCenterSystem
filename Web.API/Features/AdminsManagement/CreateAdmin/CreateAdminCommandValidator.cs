using Common.Constants;
using FluentValidation;

namespace Web.API.Features.AdminsManagement.CreateAdmin;

public class CreateAdminCommandValidator: AbstractValidator<CreateAdminCommand>
{
    public CreateAdminCommandValidator()
    {
        RuleFor(c => c.FullName)
            .NotEmpty();
        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(p => p.EmpId)
            .NotNull()
            .Must(p => p > 0);
        RuleFor(p => p.Permissions)
            .NotNull()
            .Must(p => p > 0)
            .Must(((_, i) => Enum.IsDefined(typeof(SystemPermissions), (SystemPermissions?) i!))).WithMessage("Invalid Permission");
    }
}