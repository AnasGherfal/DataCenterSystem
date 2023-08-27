using Core.Validators;
using FluentValidation;

namespace Web.API.Features.VisitsManagement.EndVisit;

public class EndVisitCommandValidator: AbstractValidator<EndVisitCommand>
{
    public EndVisitCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}