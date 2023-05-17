using Common.Helpers;
using FluentValidation;

namespace ManagementAPI.Dtos.Customer
{
    public class CreateCustomerRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string PrimaryPhone { get; set; } = string.Empty;
        public string? SecondaryPhone { get; set; }
        public string Email { get; set; } = string.Empty;
    }
    public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerRequestDto>
    {
        public CreateCustomerDtoValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name).NotEmpty()
                                .NotNull().WithMessage("يرجى إدخال اسم العميل");

             RuleFor(x => x.PrimaryPhone).NotEmpty()
                                         .NotNull().WithMessage("يرجى إدخال رقم الهاتف الرئيسي للعميل")
                                         .Must(Validation.IsValidCustomerPhoneNo).WithMessage("الرقم الذي أدخلته غير صحيح");
              RuleFor(x => x.SecondaryPhone)
                  .Must(Validation.IsValidCustomerPhoneNo).WithMessage("الرقم الذي أدخلته غير صحيح").When(x => !string.IsNullOrEmpty(x.SecondaryPhone));

            RuleFor(x => x.Email).NotEmpty()
                                 .NotNull().WithMessage("يرجى إدخال البريد الإلكتروني للعميل")
                                 .EmailAddress().WithMessage("البريد الإلكتروني الذي قمت بإدخاله غير صالح");
        }
       
    }
}
