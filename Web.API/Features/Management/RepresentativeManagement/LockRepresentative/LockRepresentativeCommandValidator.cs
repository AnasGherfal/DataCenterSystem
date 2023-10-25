using Core.Validators;
using FluentValidation;

namespace Web.API.Features.RepresentativeManagement.LockRepresentative;

public class LockRepresentativeCommandValidator: AbstractValidator<LockRepresentativeCommand>
{
    public LockRepresentativeCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}