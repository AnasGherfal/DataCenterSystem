using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.CustomerManagement.UnlockCustomer;

public class UnlockCustomerCommandValidator: AbstractValidator<UnlockCustomerCommand>
{
    public UnlockCustomerCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}