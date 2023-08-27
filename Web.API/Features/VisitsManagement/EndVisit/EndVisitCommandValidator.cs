using FluentValidation;
using Shared.Validators;

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