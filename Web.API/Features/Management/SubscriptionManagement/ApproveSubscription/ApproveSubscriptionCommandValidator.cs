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
        RuleFor(a => a.ContractDate)
            .NotEmpty()
            .Must(p => DateTime.TryParse(p, out _));
        
        RuleFor(a => a.ContractNumber)
            .NotEmpty()
            .Must(p => DateTime.TryParse(p, out _));
    }
}