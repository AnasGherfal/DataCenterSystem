using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Management.SubscriptionManagement.RejectSubscription;

public class RejectSubscriptionCommandValidator: AbstractValidator<RejectSubscriptionCommand>
{
    public RejectSubscriptionCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}