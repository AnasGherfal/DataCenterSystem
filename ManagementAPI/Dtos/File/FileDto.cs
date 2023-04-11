using FluentValidation;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;

namespace ManagementAPI.Dtos.File;

public class FileDto
{
    public IFormFile File { get; set; }
}
public class FileDtoValidator : AbstractValidator<IFormFile>
{
    public FileDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;
        When(p => IsPdf(p), () =>
        {
            RuleFor(p => p.Length).LessThanOrEqualTo(5000000);
        });
        

        RuleFor(a=>a.Length).NotEmpty().LessThanOrEqualTo(1000000)
                            .WithMessage("File size is larger than allowed");
        RuleFor(a => a.ContentType).NotEmpty().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png") || x.Equals("application/pdf"));

        
    }

    private bool IsPdf(IFormFile file)
    {
        if(file.ContentType == "application/pdf")
            return true;
        else
            return false;
    }
}
public class CustomValidator : AbstractValidator<FileDto>
{
    public CustomValidator()
    {
        RuleFor(x => x.File).SetValidator(new FileDtoValidator());
    }
}