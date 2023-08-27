using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.CustomerManagement.DeleteCustomer;

public class DeleteCustomerCommandValidator: AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}