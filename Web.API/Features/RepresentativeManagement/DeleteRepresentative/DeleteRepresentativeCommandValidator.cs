using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.RepresentativeManagement.DeleteRepresentative;

public class DeleteRepresentativeCommandValidator: AbstractValidator<DeleteRepresentativeCommand>
{
    public DeleteRepresentativeCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}