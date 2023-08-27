using Common.Helpers;
using FluentValidation;
using Shared.Dtos;

namespace ManagementAPI.Dtos.Representative;
public class UpdateRepresentativeRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IdentityNo { get; set; } = string.Empty;
    public short IdentityType { get; set; } 
    public string Email { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public FileRequestDto FirstFile { get; set; } = default!;
    public FileRequestDto SecondFile { get; set; }= default!;
}
public class EditRepresentativeValidator : AbstractValidator<UpdateRepresentativeRequestDto>
{
    [Obsolete]
    public EditRepresentativeValidator()
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

