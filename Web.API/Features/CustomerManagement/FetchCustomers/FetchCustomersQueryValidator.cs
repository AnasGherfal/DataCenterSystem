using FluentValidation;

namespace Web.API.Features.CustomerManagement.FetchCustomers;

public class FetchCustomersQueryValidator: AbstractValidator<FetchCustomersQuery>
{
    public FetchCustomersQueryValidator()
    {
            RuleFor(p => p.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(p => p.PageSize)
                .GreaterThanOrEqualTo(5);
    }
}
