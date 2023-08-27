using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.RepresentativeManagement.FetchRepresentativeById;

public class FetchRepresentativeByIdQueryValidator: AbstractValidator<FetchRepresentativeByIdQuery>
{
    public FetchRepresentativeByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .IsGuid();
    }
}
