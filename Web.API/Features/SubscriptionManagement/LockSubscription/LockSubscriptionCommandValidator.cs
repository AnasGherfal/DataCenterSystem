using FluentValidation;
using Shared.Validators;

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