using Core.Validators;
using FluentValidation;

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
