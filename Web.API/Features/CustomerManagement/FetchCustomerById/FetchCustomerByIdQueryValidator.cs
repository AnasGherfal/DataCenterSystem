using Core.Validators;
using FluentValidation;
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
