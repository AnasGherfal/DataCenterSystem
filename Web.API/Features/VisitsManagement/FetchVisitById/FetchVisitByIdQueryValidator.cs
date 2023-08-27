using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.VisitsManagement.FetchVisitById;

public class FetchVisitByIdQueryValidator: AbstractValidator<FetchVisitByIdQuery>
{
    public FetchVisitByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .IsGuid();
    }
}
