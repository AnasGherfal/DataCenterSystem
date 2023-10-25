using FluentValidation;

namespace Web.API.Features.TimeShiftManagement.FetchTimeShifts;

public class FetchTimeShiftsQueryValidator: AbstractValidator<FetchTimeShiftsQuery>
{
    public FetchTimeShiftsQueryValidator()
    {
            RuleFor(p => p.PageNumber)
                .GreaterThanOrEqualTo(1);
            RuleFor(p => p.PageSize)
                .GreaterThanOrEqualTo(5);
    }
}
