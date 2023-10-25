using Core.Validators;
using FluentValidation;

namespace Web.API.Features.SubscriptionManagement.RenewSubscription;

public class RenewSubscriptionCommandValidator: AbstractValidator<RenewSubscriptionCommand>
{
    public RenewSubscriptionCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}