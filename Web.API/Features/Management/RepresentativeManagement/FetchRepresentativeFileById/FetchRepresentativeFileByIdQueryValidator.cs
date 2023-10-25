using Core.Validators;
using FluentValidation;

namespace Web.API.Features.RepresentativeManagement.FetchRepresentativeFileById;

public class FetchRepresentativeFileByIdQueryValidator: AbstractValidator<FetchRepresentativeFileByIdQuery>
{
    public FetchRepresentativeFileByIdQueryValidator()
    {
        RuleFor(p => p.RepresentativeId)
            .NotEmpty()
            .IsGuid();
        RuleFor(p => p.FileId)
            .NotEmpty()
            .IsGuid();
    }
}
