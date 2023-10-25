using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Management.AdminsManagement.DeleteAdminById;

public class DeleteAdminByIdCommandValidator: AbstractValidator<DeleteAdminByIdCommand>
{
    public DeleteAdminByIdCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}