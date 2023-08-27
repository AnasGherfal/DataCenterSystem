using Core.Validators;
using FluentValidation;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptionFileById;

public class FetchSubscriptionFileByIdQueryValidator: AbstractValidator<FetchSubscriptionFileByIdQuery>
{
    public FetchSubscriptionFileByIdQueryValidator()
    {
        RuleFor(p => p.SubscriptionId)
            .NotEmpty()
            .IsGuid();
        RuleFor(p => p.FileId)
            .NotEmpty()
            .IsGuid();
    }
}
