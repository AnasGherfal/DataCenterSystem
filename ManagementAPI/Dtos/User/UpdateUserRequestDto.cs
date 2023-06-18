using FluentValidation;
using Infrastructure.Constants;

namespace ManagementAPI.Dtos.User;

public class UpdateUserRequestDto
{
    public string FullName { get; set; } = string.Empty;
    public int EmpId { get; set; }
    public string Email { get; set; } = string.Empty;
    public long Permissions { get; set; } = 0;
}
public class UpdateUserDtoValidator : AbstractValidator<UpdateUserRequestDto>
{
    [Obsolete("UpdateUserDto")]
    public UpdateUserDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(a => a.FullName).NotEmpty().WithMessage("fullname must be not null");
        RuleFor(a => a.Permissions).NotEmpty().WithMessage("permission must be not null")
            .GreaterThan(0).WithMessage("enter user permission");
        RuleFor(a => a.EmpId).NotEmpty().WithMessage("employee id must be not null");
        RuleFor(x => x.Email).NotEmpty().WithMessage("يرجى إدخال البريد الإلكتروني للمستخدم")
            .EmailAddress().WithMessage("البريد الإلكتروني الذي قمت بإدخاله غير صالح");
    }
}