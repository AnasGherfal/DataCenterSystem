using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.SubscriptionManagement.CreateSubscription;

public class CreateSubscriptionCommandValidator: AbstractValidator<CreateSubscriptionCommand>
{
    public CreateSubscriptionCommandValidator()
    {
        RuleFor(a => a.ServiceId)
            .IsGuid().WithMessage("service id must be not empty");
        
        RuleFor(a => a.CustomerId)
            .IsGuid().WithMessage("customer id must be not empty");
            
        RuleFor(a => a.StartDate)
            .NotEmpty().WithMessage("startdate must be not empty")
            .Must(p => DateTime.TryParse(p, out _)).WithMessage("startdate must be not empty");
        
        RuleFor(a => a.EndDate)
            .NotEmpty().WithMessage("End date must be not empty")
            .Must(p => DateTime.TryParse(p, out _)).WithMessage("End date must be not empty");

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