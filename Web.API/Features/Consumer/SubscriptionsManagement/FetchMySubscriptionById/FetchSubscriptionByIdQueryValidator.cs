using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Consumer.SubscriptionsManagement.FetchMySubscriptionById;

public class FetchSubscriptionByIdQueryValidator: AbstractValidator<FetchMySubscriptionByIdQuery>
{
    public FetchSubscriptionByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .IsGuid();
    }
}
