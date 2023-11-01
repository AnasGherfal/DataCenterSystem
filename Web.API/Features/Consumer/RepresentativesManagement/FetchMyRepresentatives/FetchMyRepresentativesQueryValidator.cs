using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Consumer.RepresentativesManagement.FetchMyRepresentatives;

public class FetchMyRepresentativesQueryValidator: AbstractValidator<FetchMyRepresentativesQuery>
{
    public FetchMyRepresentativesQueryValidator()
    {
            RuleFor(p => p.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(p => p.PageSize)
                .GreaterThanOrEqualTo(5);
    }
}
