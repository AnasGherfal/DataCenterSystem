using FluentValidation;
using ManagementAPI.Dtos.Companion;
using ManagementAPI.Dtos.Invoice;

namespace ManagementAPI.Dtos.Visit;
public class CreateVisitRequestDto
{
    public DateTime? ExpectedStartTime { get; set; }
    public DateTime? ExpectedEndTime { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Notes { get; set; }
    public string VisitTypeId { get; set; } = string.Empty;
    public string SubscriptionId { get; set; } = string.Empty;
    public IList<CreateCompanionRequestDto> Companions { get; set; } = new List<CreateCompanionRequestDto>();
    public IList<Guid> Representatives { get; set; } = new List<Guid>();
}
public class CreateVisitDtoValidator : AbstractValidator<CreateVisitRequestDto>
{
    [Obsolete("CreateVisitDto")]
    public CreateVisitDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.StartTime).NotEmpty().WithMessage("يرجى إدخال توقيت بداية الزيارة ");

        RuleFor(x => x.EndTime).NotEmpty().WithMessage("يرجى إدخال توقيت نهاية الزيارة")
                               .GreaterThan(x => x.StartTime).WithMessage("لابد أن يكون وقت نهاية الزيارة أكبر من وقت البداية");

    }
}