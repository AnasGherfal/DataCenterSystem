using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Consumer.VisitsManagement.RequestNewVisit;

public class RequestNewVisitCommandValidator: AbstractValidator<RequestNewVisitCommand>
{
    public RequestNewVisitCommandValidator()
    {
        RuleFor(p => p.ExpectedStartTime)
            .NotNull();
        RuleFor(p => p.ExpectedEndTime)
            .NotNull();
        RuleFor(p => p.SubscriptionId)
            .IsGuid();
        RuleFor(p => p.VisitType)
            .NotNull()
            .IsInEnum();
        RuleForEach(p => p.Companions)
            .SetValidator(new RequestNewVisitCommandCompanionValidator());
        RuleForEach(p => p.Representatives)
            .SetValidator(new RequestNewVisitCommandRepresentativeValidator());
    }
}

class RequestNewVisitCommandCompanionValidator : AbstractValidator<RequestNewVisitCommandCompanion>
{
    public RequestNewVisitCommandCompanionValidator()
    {
        RuleFor(p => p.FirstName)
            .NotEmpty();
        RuleFor(p => p.LastName)
            .NotEmpty();
        RuleFor(p => p.IdentityNo)
            .NotEmpty();
        RuleFor(p => p.JobTitle)
            .NotEmpty();
        RuleFor(p => p.IdentityType)
            .NotNull()
            .IsInEnum();
    }
}

class RequestNewVisitCommandRepresentativeValidator : AbstractValidator<string>
{
    public RequestNewVisitCommandRepresentativeValidator()
    {
        RuleFor(p => p)
            .IsGuid();
    }
}