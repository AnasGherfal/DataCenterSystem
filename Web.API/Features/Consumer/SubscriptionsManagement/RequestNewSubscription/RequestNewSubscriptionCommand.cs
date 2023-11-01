using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Consumer.SubscriptionsManagement.RequestNewSubscription;

public sealed record RequestNewSubscriptionCommand : IRequest<MessageResponse>
{
    public string? ServiceId { get; set; }
    public IFormFile? File { get; set; }
}