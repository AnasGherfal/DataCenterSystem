using Core.Validators;
using FluentValidation;

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