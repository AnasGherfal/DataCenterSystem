using Core.Validators;
using FluentValidation;

namespace Web.API.Features.SubscriptionManagement.UnlockSubscription;

public class UnlockSubscriptionCommandValidator: AbstractValidator<UnlockSubscriptionCommand>
{
    public UnlockSubscriptionCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}