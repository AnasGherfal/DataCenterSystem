using Core.Wrappers;
using MediatR;

namespace Web.API.Features.InvoiceManagement.CreateInvoice;

public sealed record CreateInvoiceCommand: IRequest<MessageResponse>
{
    public string? CustomerId { get; set; }
    public DateTime? IncludeVisitsFrom { get; set; }
    public DateTime? IncludeVisitsTo { get; set; }
    public string? Notes { get; set; }
}