using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.VisitsManagement.StartVisit;

public class StartVisitCommandValidator: AbstractValidator<StartVisitCommand>
{
    public StartVisitCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}