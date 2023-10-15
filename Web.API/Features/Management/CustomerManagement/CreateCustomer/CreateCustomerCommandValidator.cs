using Core.Constants;
using Core.Validators;
using FluentValidation;

namespace Web.API.Features.CustomerManagement.CreateCustomer;

public class CreateCustomerCommandValidator: AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty();
        RuleFor(a => a.Address)
            .NotEmpty();
        RuleFor(a => a.City)
            .NotEmpty();
        RuleFor(a => a.PrimaryPhone)
            .NotEmpty()
            .Matches(RegexValidation.PhoneNumber.Rule())
            .WithMessage("Phone Number is not valid, Must be +218920000000");
        When(((p, _) => !string.IsNullOrEmpty(p.SecondaryPhone)), () =>
        {
            RuleFor(a => a.SecondaryPhone)
                .Matches(RegexValidation.PhoneNumber.Rule()).WithMessage("Phone Number is not valid, Must be +218920000000");
        });
        RuleFor(a => a.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(item => item.IdentityDocument)
            .NotNull()
            .SetValidator(new DocumentFileValidator("Document File must not be null."));
        
        RuleFor(item => item.CompanyDocuments)
            .NotNull()
            .SetValidator(new DocumentFileValidator("Document File must not be null."));
    }
}