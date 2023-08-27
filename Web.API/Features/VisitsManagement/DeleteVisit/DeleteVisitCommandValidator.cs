using FluentValidation;
using Shared.Validators;

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