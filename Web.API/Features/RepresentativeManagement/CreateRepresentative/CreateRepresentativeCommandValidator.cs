using FluentValidation;
using Infrastructure.Constants;
using Shared.Validators;

namespace Web.API.Features.RepresentativeManagement.CreateRepresentative;

public class CreateRepresentativeCommandValidator: AbstractValidator<CreateRepresentativeCommand>
{
    public CreateRepresentativeCommandValidator()
    {
        RuleFor(a => a.CustomerId)
            .IsGuid().WithMessage("customer id must be not empty");
            
        RuleFor(a => a.FirstName)
            .NotEmpty().WithMessage("startdate must be not empty");
        
        RuleFor(a => a.LastName)
            .NotEmpty().WithMessage("End date must be not empty");
        
        RuleFor(a => a.IdentityNo)
            .NotEmpty().WithMessage("End date must be not empty");

        RuleFor(x => x.IdentityType)
            .NotNull().WithMessage("IdentityType is required.")
            .IsInEnum().WithMessage("IdentityType Not allowed");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid");
        
        RuleFor(p => p.PhoneNo)
            .NotEmpty().WithMessage("Phone Number must be not empty")
            .Matches(RegexValidation.PhoneNumber.Rule()).WithMessage("Phone Number is not valid, Must be +218920000000");

        RuleFor(p => p.Documents)
            .NotNull().WithMessage("Documents must be not empty")
            .NotEmpty().WithMessage("Documents must be not empty");
        
        RuleForEach(p => p.Documents)
            .SetValidator(new CreateRepresentativeCommandItemValidator());
    }
}

class CreateRepresentativeCommandItemValidator : AbstractValidator<CreateRepresentativeCommandItem>
{
    public CreateRepresentativeCommandItemValidator()
    {
        RuleFor(item => item.File)
            .SetValidator(new DocumentFileValidator("Document File must not be null."));
        
        RuleFor(item => item.DocType)
            .NotNull().WithMessage("Document Type must not be null.")
            .IsInEnum().WithMessage("Document Type must not be null.");
    }
}