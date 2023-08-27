using Core.Constants;
using Core.Wrappers;
using MediatR;

namespace Web.API.Features.SubscriptionManagement.CreateSubscription;

public sealed record CreateSubscriptionCommand : IRequest<MessageResponse>
{
    public string? ServiceId { get; set; }
    public string? CustomerId { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public IFormFile? File { get; private set; }
    public DocumentType? FileType { get; private set; }
}