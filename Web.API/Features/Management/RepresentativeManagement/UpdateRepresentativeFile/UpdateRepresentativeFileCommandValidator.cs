using Core.Validators;
using FluentValidation;

namespace Web.API.Features.RepresentativeManagement.UpdateRepresentativeFile;

public class UpdateRepresentativeFileCommandValidator: AbstractValidator<UpdateRepresentativeFileCommand>
{
    public UpdateRepresentativeFileCommandValidator()
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