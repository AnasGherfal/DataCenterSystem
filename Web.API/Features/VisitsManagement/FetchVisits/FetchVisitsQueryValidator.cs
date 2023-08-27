using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.VisitsManagement.FetchVisits;

public class FetchVisitsQueryValidator: AbstractValidator<FetchVisitsQuery>
{
    public FetchVisitsQueryValidator()
    {
            RuleFor(p => p.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(p => p.PageSize)
                .GreaterThanOrEqualTo(5);
            When(p => !string.IsNullOrWhiteSpace(p.SubscriptionId), () =>
            {
                RuleFor(p => p.SubscriptionId)
                    .IsGuid();
            });
            
            When(p => !string.IsNullOrWhiteSpace(p.CustomerId), () =>
            {
                RuleFor(p => p.CustomerId)
                    .IsGuid();
            });
    }
}
