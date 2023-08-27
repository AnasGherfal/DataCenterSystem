using FluentValidation;
using Shared.Validators;
using Web.API.Features.RepresentativeManagement.FetchRepresentativeById;

namespace Web.API.Features.CustomerManagement.FetchCustomerById;

public class FetchCustomerByIdQueryValidator: AbstractValidator<FetchRepresentativeByIdQuery>
{
    public FetchCustomerByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .IsGuid();
    }
}
