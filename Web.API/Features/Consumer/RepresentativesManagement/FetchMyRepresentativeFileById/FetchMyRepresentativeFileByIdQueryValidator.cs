using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Consumer.RepresentativesManagement.FetchMyRepresentativeFileById;

public class FetchMyRepresentativeFileByIdQueryValidator: AbstractValidator<FetchMyRepresentativeFileByIdQuery>
{
    public FetchMyRepresentativeFileByIdQueryValidator()
    {
        RuleFor(p => p.RepresentativeId)
            .NotEmpty()
            .IsGuid();
        RuleFor(p => p.FileId)
            .NotEmpty()
            .IsGuid();
    }
}
