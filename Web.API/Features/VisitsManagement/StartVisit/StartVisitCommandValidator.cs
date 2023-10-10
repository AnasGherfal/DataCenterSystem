using Core.Validators;
using FluentValidation;

namespace Web.API.Features.VisitsManagement.StartVisit;

public class StartVisitCommandValidator: AbstractValidator<StartVisitCommand>
{
    public StartVisitCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
        
        RuleFor(c => c.StartTime)
            .NotEmpty()
            .Must((p => DateTime.TryParse(p, out _)));
    }
}