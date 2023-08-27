using FluentValidation;
using Shared.Validators;

namespace Web.API.Features.InvoiceManagement.FetchInvoiceById;

public class FetchInvoiceByIdQueryValidator: AbstractValidator<ServiceManagement.FetchServiceById.FetchServiceByIdQuery>
{
    public FetchInvoiceByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .IsGuid();
    }
}
