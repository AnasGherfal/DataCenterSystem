using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Core.Validators;

public class DocumentFileValidator : AbstractValidator<IFormFile?>
{
    private readonly string _errorMessage;
    private const int MaxFileSizeInKb = 5120;
    private readonly string[] _allowedExtensions =
    {
        ".pdf",
    };

    public DocumentFileValidator(string errorMessage)
    {
        _errorMessage = errorMessage;
        RuleFor(file => file)
            .NotNull().WithMessage(_errorMessage)
            .Must(BeAValidImage).WithMessage(_errorMessage)
            .Must(HaveValidSize).WithMessage(_errorMessage);
    }

    private bool BeAValidImage(IFormFile? file)
    {
        if (file == null)
            return false;

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return _allowedExtensions.Contains(extension);
    }

    private static bool HaveValidSize(IFormFile? file)
    {
        if (file == null) return true;
        return file.Length <= MaxFileSizeInKb * 1024;
    }
}