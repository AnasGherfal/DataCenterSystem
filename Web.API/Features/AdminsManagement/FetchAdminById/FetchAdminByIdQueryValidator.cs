using Core.Validators;
using FluentValidation;

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
