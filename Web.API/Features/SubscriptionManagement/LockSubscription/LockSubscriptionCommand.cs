using Core.Wrappers;
using MediatR;

namespace Web.API.Features.SubscriptionManagement.LockSubscription;

public sealed record LockSubscriptionCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}