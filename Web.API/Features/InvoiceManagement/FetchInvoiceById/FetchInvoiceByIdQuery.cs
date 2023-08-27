using MediatR;
using Shared.Dtos;

namespace Web.API.Features.InvoiceManagement.FetchInvoiceById;
public sealed record FetchInvoiceByIdQuery: IRequest<ContentResponse<FetchInvoiceByIdQueryResponse>>
{
    public string? Id { get; set; }
}
