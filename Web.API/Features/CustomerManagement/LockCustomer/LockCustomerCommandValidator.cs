using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.CustomerManagement.LockCustomer;

public class LockCustomerCommandValidator: AbstractValidator<LockCustomerCommand>
{
    public LockCustomerCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}