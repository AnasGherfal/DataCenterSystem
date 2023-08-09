using MediatR;
using Shared.Constants;
using Shared.Dtos;

namespace Web.API.Features.SubscriptionManagement.CreateSubscription;

public sealed record CreateSubscriptionCommand : IRequest<MessageResponse>
{
    public string? ServiceId { get; set; }
    public string? CustomerId { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public IdentityType? DocType { get; private set; }
    public IFormFile? File { get; private set; }
}