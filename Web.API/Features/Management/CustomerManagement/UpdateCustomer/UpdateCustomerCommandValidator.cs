using Core.Validators;
using FluentValidation;

namespace Web.API.Features.CustomerManagement.UpdateCustomer;

public class UpdateCustomerCommandValidator: AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
    }
}