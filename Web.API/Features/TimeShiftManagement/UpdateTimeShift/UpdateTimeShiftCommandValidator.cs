using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.TimeShiftManagement.UpdateTimeShift;

public class UpdateTimeShiftCommandValidator: AbstractValidator<UpdateTimeShiftCommand>
{
    public UpdateTimeShiftCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
        RuleFor(x => x.PriceForFirstHour)
            .NotNull()
            .GreaterThan(0);
        RuleFor(x => x.PriceForRemainingHours)
            .NotNull()
            .GreaterThan(0);
        
        When((p, _) => p.StartTime != null, () =>
        {
            RuleFor(x=>x.StartTime)    
                .NotNull();
            RuleFor(x => x.EndTime)
                .NotNull();
        });
        
        When((p, _) => p.EndTime != null, () =>
        {
            RuleFor(x=>x.StartTime)    
                .NotNull();
            RuleFor(x => x.EndTime)
                .NotNull();
        });

    }
}