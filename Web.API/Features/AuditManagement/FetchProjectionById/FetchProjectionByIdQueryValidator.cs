using Core.Validators;
using FluentValidation;

namespace Web.API.Features.AuditManagement.FetchProjectionById;

public class FetchProjectionByIdQueryValidator: AbstractValidator<FetchProjectionByIdQuery>
{
    public FetchProjectionByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .IsGuid();
        
        RuleFor(p => p.ProjectionType)
            .NotNull()
            .IsInEnum();
    }
}
