using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Consumer.SubscriptionsManagement.FetchMySubscriptionFileById;

public class FetchMySubscriptionFileByIdQueryValidator: AbstractValidator<FetchMySubscriptionFileByIdQuery>
{
    public FetchMySubscriptionFileByIdQueryValidator()
    {
        RuleFor(p => p.SubscriptionId)
            .NotEmpty()
            .IsGuid();
        RuleFor(p => p.FileId)
            .NotEmpty()
            .IsGuid();
    }
}
