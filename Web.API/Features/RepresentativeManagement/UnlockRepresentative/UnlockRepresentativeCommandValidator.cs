using Core.Validators;
using FluentValidation;

namespace Web.API.Features.RepresentativeManagement.UnlockRepresentative;

public class UnlockRepresentativeCommandValidator: AbstractValidator<UnlockRepresentativeCommand>
{
    public UnlockRepresentativeCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}