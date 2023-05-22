using FluentValidation;

namespace ManagementAPI.Dtos.Subscriptions
{
    public class UpdateFileDto
    {
        public IFormFile File { get; set; }
    }
    public class UpdateFileDtoValidator:AbstractValidator<UpdateFileDto>
    {
        public UpdateFileDtoValidator()
        {
            When(p => IsPdf(p.File), () =>
            {
                RuleFor(p => p.File.Length).LessThanOrEqualTo(5000000);
            });


            RuleFor(a => a.File.Length).NotEmpty().LessThanOrEqualTo(1000000)
                                .WithMessage("File size is larger than allowed");
            RuleFor(a => a.File.ContentType).NotEmpty().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png") || x.Equals("application/pdf"));

        }
        private bool IsPdf(IFormFile file)
        {
            if (file.ContentType == "application/pdf")
                return true;
            else
                return false;
        }
    }
}
