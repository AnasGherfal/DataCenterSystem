using Core.Wrappers;
using MediatR;

namespace Web.API.Features.InvoiceManagement.FetchInvoiceById;
public sealed record FetchInvoiceByIdQuery: IRequest<ContentResponse<FetchInvoiceByIdQueryResponse>>
{
    public string? Id { get; set; }
}
