using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptionFileById;

public class FetchSubscriptionFileByIdQueryValidator: AbstractValidator<FetchSubscriptionFileByIdQuery>
{
    public FetchSubscriptionFileByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .IsGuid();
    }
}
