using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.AdminsManagement.FetchAdminById;

public class FetchAdminByIdQueryValidator: AbstractValidator<FetchAdminByIdQuery>
{
    public FetchAdminByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .IsGuid();
    }
}
