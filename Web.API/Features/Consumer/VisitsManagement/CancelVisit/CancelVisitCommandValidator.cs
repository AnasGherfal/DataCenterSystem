using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Consumer.VisitsManagement.CancelVisit;

public class CancelVisitCommandValidator: AbstractValidator<CancelVisitCommand>
{
    public CancelVisitCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}