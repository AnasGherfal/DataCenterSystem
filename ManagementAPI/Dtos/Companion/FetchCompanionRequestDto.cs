
using FluentValidation;

namespace ManagementAPI.Dtos.Companion;

public class FetchCompanionRequestDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 25;
    public int VisitId { get; set; }

}

public class FetchCompanionRequestDtoValidator : AbstractValidator<FetchCompanionRequestDto>
{
    public FetchCompanionRequestDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.VisitId).NotEmpty().WithMessage("عذرًا رقم الزيارة الذي قمت بإدخاله غير صالح");
        // PageNumber: Must be number, not less than 1.

        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);

        // PageSize: Must be number, not less than 5 & not bigger than 50

        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(5)
                                .LessThanOrEqualTo(50);

    }
}

