using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.SubscriptionManagement.UpdateSubscriptionFile;

public class UpdateSubscriptionFileCommandValidator: AbstractValidator<UpdateSubscriptionFileCommand>
{
    public UpdateSubscriptionFileCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
        
        RuleFor(x => x.DocType)
            .NotNull().WithMessage("DocType is required.")
            .IsInEnum().WithMessage("DocType Not allowed");

        RuleFor(x => x.File)
            .SetValidator(new DocumentFileValidator("File is required."));
    }
}