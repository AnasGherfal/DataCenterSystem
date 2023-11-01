using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Consumer.SubscriptionsManagement.RequestNewSubscription;

public class RequestNewSubscriptionCommandValidator: AbstractValidator<RequestNewSubscriptionCommand>
{
    public RequestNewSubscriptionCommandValidator()
    {
        RuleFor(a => a.ServiceId)
            .IsGuid();
        
        RuleFor(x => x.File)
            .SetValidator(new DocumentFileValidator("Document File Invalid"));
    }
}