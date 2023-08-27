using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.InvoiceManagement.FetchInvoices;

public class FetchInvoicesQueryValidator: AbstractValidator<FetchInvoicesQuery>
{
    public FetchInvoicesQueryValidator()
    {
            RuleFor(p => p.PageNumber)
                .GreaterThanOrEqualTo(1);
            RuleFor(p => p.PageSize)
                .GreaterThanOrEqualTo(5);
            When(p => !string.IsNullOrWhiteSpace(p.CustomerId), () =>
            {
                RuleFor(p => p.CustomerId)
                    .IsGuid();
            });
    }
}
