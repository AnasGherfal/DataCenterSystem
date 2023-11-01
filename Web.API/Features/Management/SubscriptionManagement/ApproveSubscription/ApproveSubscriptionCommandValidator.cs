using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Management.SubscriptionManagement.ApproveSubscription;

public class ApproveSubscriptionCommandValidator: AbstractValidator<ApproveSubscriptionCommand>
{
    public ApproveSubscriptionCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}