using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.CustomerManagement.FetchCustomerById
{
    public class FetchCustomerByIdQueryValidator :AbstractValidator<FetchCustomerByIdQuery>
    {
        public FetchCustomerByIdQueryValidator()
        {
            RuleFor(p => p.id)
                .NotEmpty()
                .IsGuid();
        }
    }
}
