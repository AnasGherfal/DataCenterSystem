using FluentValidation;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;

namespace ManagementAPI.Dtos.File;

public class FileDto
{
    public int ServiceId { get; set; }
    public int CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid? SubscriptionFileId { get; set; }
    public decimal? TotalPrice { get; set; }
    public IFormFile File { get; set; }

}

    public class FileDtoValidator : AbstractValidator<FileDto>
{
    public FileDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;


        When(p => IsPdf(p.File), () =>
        {
            RuleFor(p => p.File.Length).LessThanOrEqualTo(5000000);
        });
        

        RuleFor(a=>a.File.Length).NotEmpty().LessThanOrEqualTo(1000000)
                            .WithMessage("File size is larger than allowed");
        RuleFor(a => a.File.ContentType).NotEmpty().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png") || x.Equals("application/pdf"));

        
    }

    private bool IsPdf(IFormFile file)
    {
        if(file.ContentType == "application/pdf")
            return true;
        else
            return false;
    }
}
public class CustomValidator : AbstractValidator<IFormFile>
{
    public CustomValidator()
    {
        RuleFor(x => x.File).SetValidator(new FileDtoValidator());
    }
}