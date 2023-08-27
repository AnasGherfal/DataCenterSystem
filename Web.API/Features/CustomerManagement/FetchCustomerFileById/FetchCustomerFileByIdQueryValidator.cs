using Core.Validators;
using FluentValidation;

namespace Web.API.Features.CustomerManagement.FetchCustomerFileById;

public class FetchCustomerFileByIdQueryValidator: AbstractValidator<FetchCustomerFileByIdQuery>
{
    public FetchCustomerFileByIdQueryValidator()
    {
        RuleFor(p => p.CustomerId)
            .NotEmpty()
            .IsGuid();
        RuleFor(p => p.FileId)
            .NotEmpty()
            .IsGuid();
    }
}
