using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.ServiceManagement.LockService;

public class LockServiceCommandValidator: AbstractValidator<LockServiceCommand>
{
    public LockServiceCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}