using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Management.RepresentativeManagement.RejectRepresentative;

public class RejectRepresentativeCommandValidator: AbstractValidator<RejectRepresentativeCommand>
{
    public RejectRepresentativeCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}