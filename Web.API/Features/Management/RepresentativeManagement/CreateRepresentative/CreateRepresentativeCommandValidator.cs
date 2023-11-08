using Core.Constants;
using Core.Validators;
using FluentValidation;

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

        RuleFor(p => p.IdentityDocument)
            .NotNull()
            .SetValidator(new DocumentFileValidator("Identity File must not be null."));
        
        RuleFor(item => item.RepresentationDocument)
            .NotNull()
            .SetValidator(new DocumentFileValidator("Representative File must not be null."));

        RuleFor(p => p.Type)
            .NotNull()
            .IsInEnum();
        
        When((command => command.Type == RepresentativeType.Period), () =>
        {
            RuleFor(a => a.From)
                .NotEmpty()
                .Must(p => DateTime.TryParse(p, out _));
            RuleFor(a => a.To)
                .NotEmpty()
                .Must(p => DateTime.TryParse(p, out _));
        });
    }
}