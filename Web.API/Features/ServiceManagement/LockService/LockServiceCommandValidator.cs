using Core.Validators;
using FluentValidation;

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