using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.SubscriptionManagement.DeleteSubscription;

public class DeleteSubscriptionCommandValidator: AbstractValidator<DeleteSubscriptionCommand>
{
    public DeleteSubscriptionCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}