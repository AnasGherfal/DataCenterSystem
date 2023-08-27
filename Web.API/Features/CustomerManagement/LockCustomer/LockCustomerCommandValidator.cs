using Core.Validators;
using FluentValidation;

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