using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Consumer.VisitsManagement.FetchMyVisits;

public class FetchMyVisitsQueryValidator: AbstractValidator<FetchMyVisitsQuery>
{
    public FetchMyVisitsQueryValidator()
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
        
    }
}
