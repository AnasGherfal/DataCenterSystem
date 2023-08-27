using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.VisitsManagement.CreateVisit;

public class CreateVisitCommandValidator: AbstractValidator<CreateVisitCommand>
{
    public CreateVisitCommandValidator()
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
            .SetValidator(new CreateVisitCommandCompanionValidator());
        RuleForEach(p => p.Representatives)
            .SetValidator(new CreateVisitCommandRepresentativeValidator());
    }
}

class CreateVisitCommandCompanionValidator : AbstractValidator<CreateVisitCommandCompanion>
{
    public CreateVisitCommandCompanionValidator()
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

class CreateVisitCommandRepresentativeValidator : AbstractValidator<string>
{
    public CreateVisitCommandRepresentativeValidator()
    {
        RuleFor(p => p)
            .IsGuid();
    }
}