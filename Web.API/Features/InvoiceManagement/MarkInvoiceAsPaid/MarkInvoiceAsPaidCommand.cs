using Core.Dtos;
using MediatR;

namespace Web.API.Features.InvoiceManagement.MarkInvoiceAsPaid;

public sealed record MarkInvoiceAsPaidCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
    public string? AdminPassword { get; set; }
    
    public void SetId(string id)
    {
        Id = id;
    }
}