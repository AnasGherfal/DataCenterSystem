using MediatR;
using Shared.Dtos;

namespace Web.API.Features.InvoiceManagement.FetchInvoices;
public sealed record FetchInvoicesQuery: IRequest<PagedResponse<FetchInvoicesQueryResponse>>
{
    public string? CustomerId { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
