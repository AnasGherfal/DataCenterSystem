using Common.Helpers;
using FluentValidation;

namespace ManagementAPI.Dtos.Representive;
public class UpdateRepresentiveRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public int CustomerId { get; set; }
}
public class EditRepresentiveValidator : AbstractValidator<UpdateRepresentiveRequestDto>
{
    public EditRepresentiveValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.FirstName).NotEmpty()
                            .NotNull().WithMessage("يرجى إدخال الاسم الأول للمخول");
        RuleFor(x => x.LastName).NotEmpty()
                            .NotNull().WithMessage("يرجى إدخال الاسم الثاني للمخول");

        RuleFor(x => x.PhoneNo).NotEmpty()
                                    .NotNull().WithMessage("يرجى إدخال رقم الهاتف الرئيسي للعميل")
                                    .Must(Validation.IsValidCustomerPhoneNo).WithMessage("الرقم الذي أدخلته غير صحيح");
       
        RuleFor(x => x.Email).NotEmpty()
                             .NotNull().WithMessage("يرجى إدخال البريد الإلكتروني للعميل")
                             .EmailAddress().WithMessage("البريد الإلكتروني الذي قمت بإدخاله غير صالح");
     
    }
}

