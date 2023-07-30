using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.CustomerManagement.DeleteCustomerById;

public class DeleteCustomerByIdCommandValidator : AbstractValidator<DeleteCustomerByIdCommand>
{
    public DeleteCustomerByIdCommandValidator()
    {
        RuleFor(p => p.Id)
               .NotEmpty()
               .IsGuid();
    }
}
