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
        RuleFor(c => c.FileId)
            .NotEmpty()
            .IsGuid();
        
        RuleFor(x => x.DocType)
            .NotNull().WithMessage("DocType is required.")
            .IsInEnum().WithMessage("DocType Not allowed");
        
        RuleFor(x => x.File)
            .NotNull().WithMessage("File is required.")
            .Must(AcceptableFile).WithMessage("Not Acceptable file.");
    }
    
    private bool AcceptableFile(IFormFile? file)
    {
        if (file == null) return false;
        var fileExtension = Path.GetExtension(file.FileName)?.ToLower();
        var allowedExtensions = new[] { ".pdf" };
        var allowedContentTypes = new[] { "application/pdf" };
        var r = !string.IsNullOrEmpty(fileExtension) 
                && allowedExtensions.Contains(fileExtension)
                && allowedContentTypes.Contains(file.ContentType);
        var fileSizeLimit = 5 * 1024 * 1024; // 5MB
        var r2 = file.Length <= fileSizeLimit;
        return r && r2;
    }
}