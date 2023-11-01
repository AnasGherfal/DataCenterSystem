using Core.Validators;
using FluentValidation;

namespace Web.API.Features.Management.RepresentativeManagement.ApproveRepresentative;

public class ApproveRepresentativeCommandValidator: AbstractValidator<ApproveRepresentativeCommand>
{
    public ApproveRepresentativeCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}