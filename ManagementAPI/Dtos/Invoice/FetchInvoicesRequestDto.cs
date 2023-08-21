using FluentValidation;
using ManagementAPI.Dtos.Customer;

namespace ManagementAPI.Dtos.Invoice;

public record FetchInvoicesRequestDto(string CustomerName, DateTime? StartDate, DateTime? EndDate, int PageSize = 10, int PageNumber = 1);
public class FetchInvoicesRequestDtoValidator : AbstractValidator<FetchInvoicesRequestDto>
{
    [Obsolete]
    public FetchInvoicesRequestDtoValidator()
    {
        CascadeMode = CascadeMode.Stop;


        // PageNumber: Must be number, not less than 1.

        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(x => x.CustomerName).NotEmpty().WithMessage("يرجى إدخال إسم العميل لرؤية الفواتير الخاصه به..");
        RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate);

        // PageSize: Must be number, not less than 5 & not bigger than 50

        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(5)
                                .LessThanOrEqualTo(50);

    }

}