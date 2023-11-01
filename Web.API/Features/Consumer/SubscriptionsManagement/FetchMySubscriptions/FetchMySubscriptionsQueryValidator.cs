using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Consumer.SubscriptionsManagement.FetchMySubscriptions;

public class FetchMySubscriptionsQueryValidator: AbstractValidator<FetchMySubscriptionsQuery>
{
    public FetchMySubscriptionsQueryValidator()
    {
            RuleFor(p => p.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(p => p.PageSize)
                .GreaterThanOrEqualTo(5);
    }
}
