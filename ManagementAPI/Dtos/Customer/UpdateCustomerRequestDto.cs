using Common.Helpers;
using FluentValidation;
using Shared.Dtos;

namespace ManagementAPI.Dtos.Customer;

public record UpdateCustomerRequestDto(string Name, string? Address, string PrimaryPhone, string? SecondaryPhone,string Email,FileRequestDto FirstFile,FileRequestDto SecondFile);
public class EditCustomerDtoValidator : AbstractValidator<UpdateCustomerRequestDto>
{
    [Obsolete]
    public EditCustomerDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name).NotEmpty()
                            .NotNull().WithMessage("يرجى إدخال اسم العميل");

        RuleFor(x => x.PrimaryPhone).NotEmpty()
                                    .NotNull().WithMessage("يرجى إدخال رقم الهاتف الرئيسي للعميل")
                                    .Must(Validation.IsValidCustomerPhoneNo).WithMessage("الرقم الذي أدخلته غير صحيح");
        RuleFor(x => x.SecondaryPhone).Must(Validation.IsValidCustomerPhoneNo).WithMessage("الرقم الذي أدخلته غير صحيح").When(x => !string.IsNullOrEmpty(x.SecondaryPhone));

        RuleFor(x => x.Email).NotEmpty()
                             .NotNull().WithMessage("يرجى إدخال البريد الإلكتروني للعميل")
                             .EmailAddress().WithMessage("البريد الإلكتروني الذي قمت بإدخاله غير صالح");
    }
}
