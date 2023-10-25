using Core.Validators;
using FluentValidation;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptionById;

public class FetchSubscriptionByIdQueryValidator: AbstractValidator<FetchSubscriptionByIdQuery>
{
    public FetchSubscriptionByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .IsGuid();
    }
}
