﻿using Common.Helpers;
using FluentValidation;

namespace ManagementAPI.Dtos.Representative;
public class UpdateRepresentativeRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public int CustomerId { get; set; }
}
public class EditRepresentativeValidator : AbstractValidator<UpdateRepresentativeRequestDto>
{
    [Obsolete]
<<<<<<< Updated upstream:ManagementAPI/Dtos/Representive/UpdateRepresentiveRequestDto.cs
    public EditRepresentiveValidator()
=======
    public EditRepresentativeValidator()
>>>>>>> Stashed changes:ManagementAPI/Dtos/Representative/UpdateRepresentativeRequestDto.cs
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

