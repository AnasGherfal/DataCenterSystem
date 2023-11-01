using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Management.VisitsManagement.SignVisit;

public class SignVisitCommandValidator: AbstractValidator<SignVisitCommand>
{
    public SignVisitCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
        
        RuleFor(c => c.StartTime)
            .NotEmpty()
            .Must((p => DateTime.TryParse(p, out _)));
        RuleFor(c => c.EndTime)
            .NotEmpty()
            .Must((p => DateTime.TryParse(p, out _)));
    }
}