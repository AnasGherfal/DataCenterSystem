using Core.Validators;
using FluentValidation;

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