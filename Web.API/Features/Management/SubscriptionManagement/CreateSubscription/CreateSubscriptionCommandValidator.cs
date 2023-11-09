using Core.Validators;
using FluentValidation;

namespace Web.API.Features.SubscriptionManagement.CreateSubscription;

public class CreateSubscriptionCommandValidator: AbstractValidator<CreateSubscriptionCommand>
{
    public CreateSubscriptionCommandValidator()
    {
        RuleFor(a => a.ServiceId)
            .IsGuid();
        RuleFor(a => a.CustomerId)
            .IsGuid();
            
        RuleFor(a => a.StartDate)
            .NotEmpty()
            .Must(p => DateTime.TryParse(p, out _));
        
        RuleFor(a => a.EndDate)
            .NotEmpty()
            .Must(p => DateTime.TryParse(p, out _));
        
        RuleFor(a => a.ContractDate)
            .NotEmpty()
            .Must(p => DateTime.TryParse(p, out _));
        
        RuleFor(a => a.ContractNumber)
            .NotEmpty();
        
        RuleFor(x => x.File)
            .SetValidator(new DocumentFileValidator("Document File Invalid"));
    }
}