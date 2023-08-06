using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.ServiceManagement.UnlockService;

public class UnlockServiceCommandValidator: AbstractValidator<UnlockServiceCommand>
{
    public UnlockServiceCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}