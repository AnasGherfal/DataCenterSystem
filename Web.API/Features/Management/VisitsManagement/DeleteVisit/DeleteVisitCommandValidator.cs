using Core.Validators;
using FluentValidation;

namespace Web.API.Features.VisitsManagement.DeleteVisit;

public class DeleteVisitCommandValidator: AbstractValidator<DeleteVisitCommand>
{
    public DeleteVisitCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}