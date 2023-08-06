using Common.Constants;
using FluentValidation;

namespace Web.API.Features.ServiceManagement.CreateService;

public class CreateServiceCommandValidator: AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("يرجى اختيار خدمة")
            .MinimumLength(3).WithMessage("name must be minimum lenght is 3 letters")
            .MaximumLength(100);
        RuleFor(x=>x.AmountOfPower)    
            .NotEmpty().WithMessage("Amount of power must be not null");
        RuleFor(x => x.AcpPort)
            .NotEmpty().WithMessage("AcpPort must be not null");
        RuleFor(x => x.Dns)
            .NotEmpty().WithMessage("Dnd must be not null");
        RuleFor(x => x.MonthlyVisits)
            .NotNull().WithMessage("monthly visit must be not null")
            .GreaterThan(0).WithMessage(" monthly visitis must be digit"); ;
        RuleFor(x => x.Price)
            .NotNull().WithMessage("price must be not null")
            .GreaterThan(0).WithMessage(" price must be digit");
        RuleFor(x => x.File)
            .NotNull().WithMessage("File is required.")
            .Must(BeAValidFile).WithMessage("Invalid file.")
            .Must(BeWithinFileSizeLimit).WithMessage("File size exceeds the allowed limit.")
            .Must(BeWithAllowedExtensions).WithMessage("File extension is not allowed.");
    }
    
    
    private bool BeAValidFile(IFormFile? file)
    {
        if (file == null) return false;
        var fileExtension = Path.GetExtension(file.FileName)?.ToLower();
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var allowedContentTypes = new[] { "image/jpeg", "image/png" };
        return !string.IsNullOrEmpty(fileExtension) 
               && allowedExtensions.Contains(fileExtension)
               && allowedContentTypes.Contains(file.ContentType);
    }

    private bool BeWithinFileSizeLimit(IFormFile? file)
    {
        if (file == null) return false; // Skip validation if file is not provided
        var fileSizeLimit = 5 * 1024 * 1024; // 5MB
        return file.Length <= fileSizeLimit;
    }

    private bool BeWithAllowedExtensions(IFormFile? file)
    {
        if (file == null) return false; // Skip validation if file is not provided
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var fileExtension = Path.GetExtension(file.FileName)?.ToLower();
        return !string.IsNullOrEmpty(fileExtension) && allowedExtensions.Contains(fileExtension);
    }
}