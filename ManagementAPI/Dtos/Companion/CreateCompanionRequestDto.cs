using FluentValidation;

namespace ManagementAPI.Dtos.Companion;

public class CreateCompanionRequestDto
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string IdentityNo { get; set; } = String.Empty;
    public short IdentityType { get; set; }
    public string? JobTitle { get; set; } = String.Empty;
    public int VisitId { get; set; }
}
public class CreateCompanionRequestDtoValidator : AbstractValidator<CreateCompanionRequestDto>
{
    public CreateCompanionRequestDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.VisitId).NotEmpty().WithMessage("عذرًا رقم الزيارة الذي قمت بإدخاله غير صالح");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("يرجى إدخال الاسم الأول للمرافق")
                                  .MaximumLength(50).WithMessage(" لقد تجاوزت الحد الأقصى للحروف للاسم الأول"); ;
        RuleFor(x => x.LastName).NotEmpty().WithMessage("يرجى إدخال الاسم الثاني للمرافق")
                                .MaximumLength(50).WithMessage("لقد تجاوزت الحد الأقصى للحروف للاسم الثاني");


    }
}
