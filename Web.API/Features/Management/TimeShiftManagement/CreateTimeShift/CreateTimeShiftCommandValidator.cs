using FluentValidation;

namespace Web.API.Features.TimeShiftManagement.CreateTimeShift;

public class CreateTimeShiftCommandValidator: AbstractValidator<CreateTimeShiftCommand>
{
    public CreateTimeShiftCommandValidator()
    {
        When((p, _) => string.IsNullOrEmpty(p.Date), () =>
        {
            RuleFor(x => x.Day)
                .NotNull()
                .IsInEnum();
            RuleFor(x=>x.StartTime)    
                .NotNull();
            RuleFor(x => x.EndTime)
                .NotNull();
        });
        When((p, _) => p.Day == null, () =>
        {
            RuleFor(x => x.Date)
                .NotNull();
        });
        RuleFor(x => x.PriceForFirstHour)
            .NotNull()
            .GreaterThan(0);
        RuleFor(x => x.PriceForRemainingHours)
            .NotNull()
            .GreaterThan(0);
    }
}