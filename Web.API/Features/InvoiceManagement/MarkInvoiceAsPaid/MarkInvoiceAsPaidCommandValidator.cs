using Core.Validators;
using FluentValidation;

namespace Web.API.Features.InvoiceManagement.MarkInvoiceAsPaid;

public class MarkInvoiceAsPaidCommandValidator: AbstractValidator<MarkInvoiceAsPaidCommand>
{
    public MarkInvoiceAsPaidCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .IsGuid();
        
        RuleFor(c => c.AdminPassword)
            .NotEmpty();
    }
}