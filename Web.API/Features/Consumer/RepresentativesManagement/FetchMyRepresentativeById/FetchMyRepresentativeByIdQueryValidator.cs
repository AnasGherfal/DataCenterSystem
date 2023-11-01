using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Consumer.RepresentativesManagement.FetchMyRepresentativeById;

public class FetchMyRepresentativeByIdQueryValidator: AbstractValidator<FetchMyRepresentativeByIdQuery>
{
    public FetchMyRepresentativeByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .IsGuid();
    }
}
