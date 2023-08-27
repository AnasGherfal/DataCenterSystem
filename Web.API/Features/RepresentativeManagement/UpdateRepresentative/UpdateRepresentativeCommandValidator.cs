using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.RepresentativeManagement.UpdateRepresentative;

public class UpdateRepresentativeCommandValidator: AbstractValidator<UpdateRepresentativeCommand>
{
    public UpdateRepresentativeCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}