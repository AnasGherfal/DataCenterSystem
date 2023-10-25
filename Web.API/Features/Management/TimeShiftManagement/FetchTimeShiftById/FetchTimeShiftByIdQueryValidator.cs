using Core.Validators;
using FluentValidation;

namespace Web.API.Features.TimeShiftManagement.FetchTimeShiftById;

public class FetchTimeShiftByIdQueryValidator: AbstractValidator<FetchTimeShiftByIdQuery>
{
    public FetchTimeShiftByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .IsGuid();
    }
}
