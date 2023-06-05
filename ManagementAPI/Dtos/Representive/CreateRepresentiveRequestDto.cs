using Common.Helpers;
using FluentValidation;
using ManagementAPI.Dtos.Customer;

namespace ManagementAPI.Dtos.Representive;

public class CreateRepresentiveRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IdentityNo { get; set; } = string.Empty;
    public short IdentityType { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public int CustomerId { get; set; }
   // public IList<FileRequestDto> Files { get; set; }
}
public class CreateRepresentiveDtoValidator : AbstractValidator<CreateRepresentiveRequestDto>
{
    public CreateRepresentiveDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.FirstName).NotEmpty()
                            .NotNull().WithMessage("يرجى إدخال الاسم الاول للمخول");
        RuleFor(x => x.LastName).NotEmpty()
                            .NotNull().WithMessage("يرجى إدخال اسم العائلة للمخول");

        RuleFor(x => x.PhoneNo).NotEmpty()
                                    .NotNull().WithMessage("يرجى إدخال رقم الهاتف الخاص بالمخول")
                                    .Must(Validation.IsValidCustomerPhoneNo).WithMessage("الرقم الذي أدخلته غير صحيح");
        
        RuleFor(x => x.Email).NotEmpty()
                             .NotNull().WithMessage("يرجى إدخال البريد الإلكتروني للعميل")
                             .EmailAddress().WithMessage("البريد الإلكتروني الذي قمت بإدخاله غير صالح");
        RuleFor(x => x.IdentityNo).NotEmpty()
                            .NotNull().WithMessage("يرجى إدخال رقم إثبات الهوية الخاص بالمخول");

       
    }
}
