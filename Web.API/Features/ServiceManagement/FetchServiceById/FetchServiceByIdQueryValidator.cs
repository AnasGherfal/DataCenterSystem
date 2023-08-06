using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.ServiceManagement.FetchServiceById;

public class FetchServiceByIdQueryValidator: AbstractValidator<FetchServiceByIdQuery>
{
    public FetchServiceByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .IsGuid();
    }
}
