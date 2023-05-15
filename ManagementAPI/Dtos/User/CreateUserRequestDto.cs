using FluentValidation;
using Infrastructure.Constants;

namespace ManagementAPI.Dtos.User;

public class CreateUserRequestDto
{
    public string FullName { get; set; } = string.Empty;
    public long Permission { get; set; } = 0;
    public int EmpId { get; set; }
    public string Email { get; set; } = string.Empty;
}
public class CreateUserDtoValidator : AbstractValidator<CreateUserRequestDto>
{
    public CreateUserDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(a => a.FullName).NotEmpty().WithMessage("fullname must be not null");
        RuleFor(a => a.Permission).NotEmpty().WithMessage("permission must be not null")
            .GreaterThan(0).WithMessage("enter user permission");
        RuleFor(a => a.EmpId).NotEmpty().WithMessage("employee id must be not null");
       /* RuleFor(a => a.Password).NotEmpty().WithMessage("password must be not null")
            .MinimumLength(8).WithMessage("password must be more than 8 letters")
            .MaximumLength(127).WithMessage("password must be less than 127 letters");*/
        RuleFor(x => x.Email).NotEmpty()
            .NotNull().WithMessage("يرجى إدخال البريد الإلكتروني للمستخدم");
            //.EmailAddress().WithMessage("البريد الإلكتروني الذي قمت بإدخاله غير صالح");
    }
}