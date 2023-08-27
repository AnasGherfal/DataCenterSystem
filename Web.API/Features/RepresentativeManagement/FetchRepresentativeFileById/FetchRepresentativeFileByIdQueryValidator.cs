using FluentValidation;
using Shared.Validators;

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
