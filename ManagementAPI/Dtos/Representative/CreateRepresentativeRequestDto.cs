using Common.Helpers;
using FluentValidation;
using ManagementAPI.Dtos.Customer;
using Shared.Dtos;

namespace ManagementAPI.Dtos.Representative;

public class CreateRepresentativeRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string IdentityNo { get; set; } = string.Empty;
    public short IdentityType { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public FileRequestDto FilesHandler { get; set; } = default!;
}
public class CreateRepresentativeDtoValidator : AbstractValidator<CreateRepresentativeRequestDto>
{
    [Obsolete("CreateRepresentativeDto")]
    public CreateRepresentativeDtoValidator()
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
