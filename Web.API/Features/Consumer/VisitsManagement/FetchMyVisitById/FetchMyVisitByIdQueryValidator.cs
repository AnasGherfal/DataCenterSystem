using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Consumer.VisitsManagement.FetchMyVisitById;

public class FetchMyVisitByIdQueryValidator: AbstractValidator<FetchMyVisitByIdQuery>
{
    public FetchMyVisitByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .IsGuid();
    }
}
