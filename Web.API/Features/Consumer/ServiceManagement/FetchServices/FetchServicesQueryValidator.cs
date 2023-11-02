using FluentValidation;

namespace Web.API.Features.Consumer.ServiceManagement.FetchServices;

public class FetchServicesQueryValidator: AbstractValidator<FetchServicesQuery>
{
    public FetchServicesQueryValidator()
    {
            RuleFor(p => p.PageNumber)
                .GreaterThanOrEqualTo(1);
            RuleFor(p => p.PageSize)
                .GreaterThanOrEqualTo(5);
    }
}
