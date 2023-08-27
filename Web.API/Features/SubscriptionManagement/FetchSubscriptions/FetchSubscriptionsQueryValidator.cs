using Core.Validators;
using FluentValidation;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptions;

public class FetchSubscriptionsQueryValidator: AbstractValidator<FetchSubscriptionsQuery>
{
    public FetchSubscriptionsQueryValidator()
    {
            RuleFor(p => p.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(p => p.PageSize)
                .GreaterThanOrEqualTo(5);
            When(p => !string.IsNullOrWhiteSpace(p.CustomerId), () =>
            {
                RuleFor(p => p.CustomerId)
                    .IsGuid();
            });
    }
}
