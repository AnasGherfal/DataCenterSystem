using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.TimeShiftManagement.DeleteTimeShift;

public class DeleteTimeShiftCommandValidator: AbstractValidator<DeleteTimeShiftCommand>
{
    public DeleteTimeShiftCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}