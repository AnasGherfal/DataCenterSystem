using FluentValidation;
using ManagementAPI.Dtos.Customer;

namespace ManagementAPI.Dtos.Invoice;

public record CreateInvoiceRequestDto(DateTime Date, string? Description, string? InvoiceNo, DateTime StartDate, DateTime EndDate, int SubscriptionId);

public class CreateInvoiceDtoValidator : AbstractValidator<CreateInvoiceRequestDto>
{
    [Obsolete("CreateInvoiceDto")]
    public CreateInvoiceDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.StartDate).NotEmpty().WithMessage("يرجى تحديد تاريخ بداية الزيارات المراد تضمينها في الفاتورة ");

        RuleFor(x => x.EndDate).NotEmpty().WithMessage("يرجى تحديد تاريخ نهاية الزيارات المراد تضمينها في الفاتورة")
                               .GreaterThan(x => x.StartDate).WithMessage("لابد أن يكون تاريخ النهاية أكبر من تاريخ البداية");
        RuleFor(x => x.SubscriptionId).NotEmpty().WithMessage("يرجى تحديد رقم الأشتراك الخاص بالزبون");

    }
}

