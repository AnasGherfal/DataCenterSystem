using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.AdminsManagement.DeleteAdminById;

public class DeleteAdminByIdCommandValidator: AbstractValidator<DeleteAdminByIdCommand>
{
    public DeleteAdminByIdCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}