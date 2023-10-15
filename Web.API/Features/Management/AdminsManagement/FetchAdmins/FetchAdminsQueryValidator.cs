using FluentValidation;

namespace Web.API.Features.Management.AdminsManagement.FetchAdmins;

public class FetchAdminsQueryValidator: AbstractValidator<FetchAdminsQuery>
{
    public FetchAdminsQueryValidator()
    {
            RuleFor(p => p.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(p => p.PageSize)
                .GreaterThanOrEqualTo(5);
    }
}
