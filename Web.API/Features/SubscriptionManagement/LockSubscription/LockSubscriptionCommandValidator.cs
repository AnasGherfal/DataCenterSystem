using Core.Validators;
using FluentValidation;

namespace Web.API.Features.SubscriptionManagement.LockSubscription;

public class LockSubscriptionCommandValidator: AbstractValidator<LockSubscriptionCommand>
{
    public LockSubscriptionCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}