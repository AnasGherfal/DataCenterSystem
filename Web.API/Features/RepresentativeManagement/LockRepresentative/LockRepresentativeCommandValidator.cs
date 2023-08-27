using FluentValidation;
using Shared.Validators;

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